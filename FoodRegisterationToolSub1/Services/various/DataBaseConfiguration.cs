using System;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//*****************************************************Database Configuration file managed in Program.c*****************************************************************************************************************************************************************************************************************************************************************************/
/// <summary>
/// This setting the database configuration to the an enviroment variabel defined in the server with the following value: 
/// DB_HOST Host of the database server in most know database servide porvider
/// DB_PORT the port of the database given
/// DB_USER user of the databse 
/// DB_NAME name of the database 
/// 
/// In this case we have only one database access, in more advanced program several database configuration could be a good choice.
/// This value work automatically with the postgres database wich is set up locally. Ensure the data base running in : 
/// 
/// Localhost withing the port 5432. Name of the user and password should be default. 
/// User postgres Password 12345 and DatabaseNAme : FoodRegistration
/// </summary>
public static class DataBaseConfiguration {
    
    public static IServiceCollection ConfigurationDatabase (IServiceCollection service) {

        Env.Load();
        
        var dbHost = Env.GetString("DB_HOST")           ?? "localhost";
        var dbPort = Env.GetString("DB_PORT")           ?? "5432";
        var dbUser = Env.GetString("DB_USER")           ?? "postgres";
        var dbPassword = Env.GetString("DB_PASSWORD")   ?? "12345";
        var dbName =Env.GetString("DB_NAME")            ?? "FoodRegistration";

        var connectionString = $"Host={dbHost};Port={dbPort};Username={dbUser};Password={dbPassword};Database={dbName}";

        service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        return service;
        

    }
}
