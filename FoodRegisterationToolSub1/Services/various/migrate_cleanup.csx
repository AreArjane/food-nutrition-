// <summary>
/// Handles the process of applying Entity Framework Core database migrations and cleaning up migration files.
/// </summary>
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;


var migrationDir = Path.Combine(Directory.GetCurrentDirectory(), "Migrations");
/// <summary>
/// Configures the service collection for database access.
/// </summary>
var serviceCollection = new ServiceCollection();

DataBaseConfiguration.ConfigurationDatabase(serviceCollection);


using(var serviceProvider = serviceCollection.BuildServiceProvider())
using(var context = serviceProvider.GetRequiredService<ApplicationDbContext>()) {

    try {
           /// <summary>
        /// Applies pending migrations to the database and outputs the result to the console.
        /// </summary>
        Console.WriteLine("Apply the migration to Database.........");
        context.Database.Migrate();
        Console.WriteLine("Migration applied succeffuly");
          /// <summary>
        /// Checks if the migrations directory exists and deletes the migration files to clean up.
        /// </summary>
        if (Directory.Exists(migrationDir)) {
            Console.WriteLine("Deleting Migrations directory Log...");
            var files = Directory.GetFiles(migrationDir, "*.cs");
            foreach (var file in files) {
                File.Delete(file);
                Console.WriteLine($"Delete {file}");
            }
        } else { 
              /// <summary>
            /// Logs a message if the migrations directory is not found.
            /// </summary>
            Console.WriteLine("Migrations Directory not found....!");
        }
    } catch(Exception e) {
/// <summary>
        /// Handles and logs any exceptions that occur during the migration process.
        /// </summary>
        Console.WriteLine($"Error : {e.Message}");
    }
}
