using System;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration;

/// <summary>
/// Configuration helper pour initialiser et migrer les deux bases de données
/// </summary>
public static class DatabaseConfiguration
{
    /// <summary>
    /// Initialiser les deux bases de données au démarrage
    /// </summary>
    public static async Task InitializeDatabasesAsync(DABDbContext dabContext, IAMDbContext iamContext)
    {
        try
        {
            // Migrate DAB Database
            await dabContext.Database.MigrateAsync();
            Console.WriteLine("✓ DAB Database migrated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error migrating DAB Database: {ex.Message}");
        }

        try
        {
            // Migrate IAM Database
            await iamContext.Database.MigrateAsync();
            Console.WriteLine("✓ IAM Database migrated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error migrating IAM Database: {ex.Message}");
        }
    }

    /// <summary>
    /// Vérifier la connexion aux deux bases de données
    /// </summary>
    public static async Task<bool> CheckDatabaseConnectionAsync(DABDbContext dabContext, IAMDbContext iamContext)
    {
        try
        {
            var dabConnected = await dabContext.Database.CanConnectAsync();
            var iamConnected = await iamContext.Database.CanConnectAsync();

            if (dabConnected)
                Console.WriteLine("✓ Connected to DAB Database");
            else
                Console.WriteLine("✗ Failed to connect to DAB Database");

            if (iamConnected)
                Console.WriteLine("✓ Connected to IAM Database");
            else
                Console.WriteLine("✗ Failed to connect to IAM Database");

            return dabConnected && iamConnected;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Database connection error: {ex.Message}");
            return false;
        }
    }
}
