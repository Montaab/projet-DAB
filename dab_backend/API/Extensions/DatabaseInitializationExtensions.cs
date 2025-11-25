using System;
using DAL.Configuration;
using DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions;

/// <summary>
/// Extension pour initialiser et vérifier les bases de données au démarrage
/// </summary>
public static class DatabaseInitializationExtensions
{
    /// <summary>
    /// Vérifie et initialise les bases de données au démarrage de l'application
    /// </summary>
    public static async Task InitializeDatabasesAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dabContext = scope.ServiceProvider.GetRequiredService<DABDbContext>();
            var iamContext = scope.ServiceProvider.GetRequiredService<IAMDbContext>();

            Console.WriteLine("\n========== Database Initialization ==========");

            // Vérifier les connexions
            var connectionOk = await DatabaseConfiguration.CheckDatabaseConnectionAsync(dabContext, iamContext);

            if (connectionOk)
            {
                Console.WriteLine("\n-> Initializing databases...\n");
                // Initialiser les migrations si nécessaire
                await DatabaseConfiguration.InitializeDatabasesAsync(dabContext, iamContext);
                Console.WriteLine("\n✓ All databases initialized successfully!");
            }
            else
            {
                Console.WriteLine("\n✗ Database connection failed. Please check your configuration.");
            }

            Console.WriteLine("============================================\n");
        }
    }
}
