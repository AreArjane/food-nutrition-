// Load environment variables and required assemblies
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DotNetEnv;


using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodRegisterationToolSub1;
var migrationDir = Path.Combine(Directory.GetCurrentDirectory(), "Migrations");
var serviceCollection = new ServiceCollection();
//Database configuration
DataBaseConfiguration.ConfigurationDatabase(serviceCollection);


using(var serviceProvider = serviceCollection.BuildServiceProvider())
using(var context = serviceProvider.GetRequiredService<ApplicationDbContext>()) {

    try {
        Console.WriteLine("Apply the migration to Database.........");
        context.Database.Migrate();
        Console.WriteLine("Migration applied succeffuly");

        if (Directory.Exists(migrationDir)) {
            Console.WriteLine("Deleting Migrations directory Log...");
            var files = Directory.GetFiles(migrationDir, "*.cs");
            foreach (var file in files) {
                File.Delete(file);
                Console.WriteLine($"Delete {file}");
            }
        } else { 
            Console.WriteLine("Migrations Directory not found....!");
        }
    } catch(Exception e) {
        Console.WriteLine($"Error : {e.Message}");
    }
}