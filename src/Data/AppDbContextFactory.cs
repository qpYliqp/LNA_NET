using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data;

public class AppDbContextFactory
{
    
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            // Assumons que la chaîne de connexion est dans le fichier appsettings.json du projet API
            // Pour que cela fonctionne, vous devez exécuter la commande 'dotnet ef'
            // depuis le répertoire du projet qui contient le DbContext (Data) 
            // et utiliser --startup-project pour pointer vers l'API.
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("ATTENTION: Utilisation d'une chaîne de connexion factice pour le Design-Time.");
            connectionString = "Host=localhost;Database=design_time_db;Username=user;Password=password"; 
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
    
}