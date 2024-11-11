
using Microsoft.Extensions.DependencyInjection;
using FoodRegisterationToolSub1;

var builder = WebApplication.CreateBuilder(args);



//Database configuration
DataBaseConfiguration.ConfigurationDatabase(builder.Services);
//Import dataset to database
builder.Services.AddScoped<DataSetImporter>();




//***********************Container View********************************//
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
//*******************************************************//
//Add import service

using (var scope = app.Services.CreateAsyncScope()) {
    var services = scope.ServiceProvider;
    try {
        var dataImporter = services.GetRequiredService<DataSetImporter>();
        dataImporter.ImportData();
        Console.WriteLine("Data imported successfully");
    }

    catch (Exception e) {
        Console.WriteLine($"An error occured with importing dataset into database {e.Message}");
    }
}




//*********************Application Loop**********************************//
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//*******************************************************//
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Login", action = "Login" });

app.Run();
