using Microsoft.EntityFrameworkCore;
using FoodRegistrationToolSub1.Models.datasets;
using DotNetEnv;

Env.Load();


var builder = WebApplication.CreateBuilder(args);

// Load environment variables
var dbHost = Env.GetString("DB_HOST") ?? "localhost";
var dbPort = Env.GetString("DB_PORT") ?? "5432";
var dbUser = Env.GetString("DB_USER") ?? "your_default_user";
var dbPassword = Env.GetString("DB_PASSWORD") ?? "your_default_password";
var dbName =Env.GetString("DB_NAME") ?? "neondb";

// Build the connection string
var connectionString = $"Host={dbHost};Port={dbPort};Username={dbUser};Password={dbPassword};Database={dbName}";

// Register ApplicationDbContext with the DI container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
Console.WriteLine("Connection String: " + connectionString);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
