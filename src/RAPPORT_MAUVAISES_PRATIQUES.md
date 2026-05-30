# Audit du projet — mauvaises pratiques & corrections

## 1. Erreurs bloquantes (le projet ne compilait pas)

1. **Dépendance circulaire / inversion de dépendance violée.** Les DTOs du projet `Domain`
   (`CreateBookDto.ToEntity()`, etc.) référençaient les entités EF de `Infrastructure`
   (`Book`, `BookStep`), alors que `Domain` ne référence pas `Infrastructure`. Le cœur métier
   dépendait de la couche de persistance : impossible à compiler et architecture inversée.
2. **Mappers inexistants.** Tous les handlers faisaient `using Application.Mappers;` et appelaient
   `entity.ToDto()` — ni le projet `Application`, ni les méthodes `ToDto` n'existaient.
3. **Namespaces incohérents.** `Infrastructure.csproj` imposait `<RootNamespace>Data</RootNamespace>`
   mais les fichiers déclaraient `namespace Infrastructure`. Les handlers importaient `using Data;`,
   `using Data.Entities;`, `using Data.Specifications;` → types introuvables.
4. **MediatR scannait le mauvais assembly.** `RegisterServicesFromAssembly(typeof(IMinioService).Assembly)`
   pointe sur `Domain`, alors que tous les handlers vivent dans `API`. Aucun handler n'aurait été
   enregistré → erreur runtime sur chaque requête.
5. **Package `Myth.Specification` manquant dans `Infrastructure`** (la spec l'utilisait) et présent
   inutilement dans `Domain`.

## 2. Bugs métier

6. **`UpdateBookHandler` faisait `Books.Add(book)` au lieu d'une mise à jour** : il créait un doublon
   au lieu de modifier l'existant. La validation des étapes était entièrement commentée.
7. **`UpdateBookDto.BookSteps` était typé `ICollection<UpdateBookDto>`** (auto-référence) au lieu de
   `UpdateBookStepDto`.
8. **`CreateBookHandler` comptait deux fois `ProductionSteps`** (requête SQL dupliquée inutile).
9. **Incohérence de type sur `Price`** : `int?` dans les DTOs, `float?` dans l'entité.
10. **Projection EF non traduisible** : `.Select(b => b.ToDto())` dans une requête `IQueryable`
    aurait levé une exception à l'exécution (appel de méthode non traduisible en SQL).

## 3. Mauvaises pratiques transverses

11. **Fuite d'informations** : le middleware renvoyait systématiquement `exception.StackTrace` au client.
12. **Logique métier dans l'entité de persistance** (`Book.AddBookStep`, `updateGlobalStatus`) :
    couple le métier à EF Core.
13. **Code mort** : `IBookRepository`/`BookRepository`/`AuthorRepository` vides, `DeleteBookHandler`
    vide, `GetAllBookWithAuthorQuery` sans handler, `BookException` classe vide non dérivée d'`Exception`.
14. **`Exception` générique** levée pour des règles métier (`throw new Exception(...)`) au lieu d'un type dédié.
15. **`Database.Migrate()` au démarrage** : incompatible avec une approche database-first.
16. **Nommage incohérent** : routes `books` vs `api/[controller]`, paramètres `prefix`/`bookId` en minuscule.

## 4. Corrections apportées

- **DTO → Models + Mappers.** Création de `Domain/Models` (modèles de domaine purs, sans EF), et de
  `Infrastructure/Mappers` (extensions `ToModel`/`ToEntity`). Les handlers retournent désormais des
  modèles de domaine. Suppression de `Domain/DTOs`.
- **Objets Request pour les commandes.** Les controllers reçoivent `CreateBookRequest` /
  `UpdateBookRequest`, mappés vers les `Command` MediatR. Les requêtes EF de projection ont été
  matérialisées avant mapping en mémoire.
- **Logique métier déplacée sur `Domain.Models.Book`** (`AddBookStep`, `UpdateGlobalStatus`) + enum
  `ProductionStatus` au lieu de nombres magiques.
- **Erreurs corrigées** : namespaces unifiés sur `Infrastructure`, MediatR scanne l'assembly `API`,
  `UpdateBookHandler` met réellement à jour, doublon de comptage supprimé, `Price` en `float?`,
  exception métier `BookDomainException` (→ HTTP 400), middleware ne fuit la stack trace qu'en `Development`.
- **Database-first** : suppression du dossier `Migrations` et de `Database.Migrate()`.
- **Nettoyage** : suppression du code mort (repositories vides, DeleteBook, GetAllBookWithAuthor).

## 5. Passage en database-first avec EF Core Power Tools (à faire de ton côté)

L'infrastructure est prête. Pour régénérer entités + configurations depuis ta base :

```
# Depuis le projet Infrastructure (clic droit > EF Core Power Tools > Reverse Engineer)
# ou en CLI :
dotnet ef dbcontext scaffold "Host=...;Database=...;Username=...;Password=..." \
    Npgsql.EntityFrameworkCore.PostgreSQL \
    --context AppDbContext \
    --output-dir Entities \
    --context-dir . \
    --use-database-names \
    --no-onconfiguring \
    --data-annotations \
    --force
```

Recommandations EFPT : cocher *Use IEntityTypeConfiguration*, *Put DbContext and entities in separate
namespaces*, décocher *Include connection string in generated code*. Les seeds (`Infrastructure/Seeds/*.sql`)
sont à appliquer directement en base, et non via migration.

> Remarque : le sandbox ne dispose pas du SDK .NET 10, je n'ai donc pas pu compiler. À valider par un
> `dotnet build` de ton côté, en particulier l'API exacte de `Myth.Specification` (`SpecBuilder`/`Predicate`).
