using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<ProductionStep> ProductionSteps { get; set; }
    public DbSet<BookStep> BookSteps { get; set; }
    public DbSet<Status> Status { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //ApplyConfigurationFromAssembly --> fournie par EF --> Exécute une tâche spécifique lors de la phase de construction du modèle de données.
        //Scan Infrastructure.dll pour trouver les classes qui implémentent IEntityTypeConfiguration<Entity>
        //Pour chaque classe trouvé --> applique la méthode Configure()
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);    }
    
}