using Domain.Models;

namespace API.Features.Books.Commands.CreateBook;

/// <summary>
/// Contrat d'entrée HTTP pour la création d'un livre.
/// </summary>
public record CreateBookRequest(
    string Title,
    string Isbn,
    string? Nuart,
    int? Pages,
    float? Price,
    string? Note,
    string? Summary,
    string? Hook,
    string? Marketing,
    DateTime? ReleaseDate,
    ICollection<int> AuthorsId,
    ICollection<CreateBookStepRequest> BookSteps)
{
    /// <summary>Construit le modèle de domaine (sans les étapes, ajoutées par le handler pour validation).</summary>
    public Book ToModel() => new()
    {
        Title = Title,
        Isbn = Isbn,
        Nuart = Nuart,
        Pages = Pages,
        Price = Price,
        Note = Note,
        Summary = Summary,
        Hook = Hook,
        Marketing = Marketing,
        ReleaseDate = ReleaseDate
    };
}

public record CreateBookStepRequest(int ProductionStepId, int StatusId, DateTime EndDate)
{
    public BookStep ToModel() => new()
    {
        ProductionStepId = ProductionStepId,
        StatusId = StatusId,
        EndDate = EndDate
    };
}
