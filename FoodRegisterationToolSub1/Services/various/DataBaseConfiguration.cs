using System;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DataBaseConfiguration {
    


    public static IServiceCollection ConfigurationDatabase (IServiceCollection service) {

        Env.Load();
        // Load environment variables
        var dbHost = Env.GetString("DB_HOST") ?? "localhost";
        var dbPort = Env.GetString("DB_PORT") ?? "5432";
        var dbUser = Env.GetString("DB_USER") ?? "your_default_user";
        var dbPassword = Env.GetString("DB_PASSWORD") ?? "your_default_password";
        var dbName =Env.GetString("DB_NAME") ?? "neondb";

        var connectionString = $"Host={dbHost};Port={dbPort};Username={dbUser};Password={dbPassword};Database={dbName}";

        service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        return service;
        

    }
}