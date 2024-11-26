
using Microsoft.Extensions.DependencyInjection;
using FoodRegisterationToolSub1;
using Microsoft.AspNetCore.Identity;
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCoreRateLimit;
using EmailService;

var builder = WebApplication.CreateBuilder(args);

//*********************Rate Limit Configuration*************************************************************************************************************************************************************************************************************************************************//
    /**    builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
        builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        builder.Services.AddHttpContextAccessor();**/
//*****************************************************Cookies Configuration*****************************************************************************************************************************************************************************************************************************************************************************/

//Database configuration
DataBaseConfiguration.ConfigurationDatabase(builder.Services);
//Import dataset to database
builder.Services.AddScoped<DataSetImporter>();




//***********************Container View********************************************************************************************************************************************************************************************************************************//

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    {
        //WithOrigins("http://localhost:3000")
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(options => {
    //options.JsonSerializerOptions.PropertyNamingPolicy =null;
    options.JsonSerializerOptions.WriteIndented = true;
});

/***********************Building the app ***************************************************************************************************************************************************************************************************************************************************************/

var app = builder.Build();


//wait EmailVerification.Execute();
//***************************************************************************************************************************************************************************************************************************************************************************************************//
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

//*********************Rate Limit Implementing******************************************************************************************************************************************************************************************************************//

    //app.UseIpRateLimiting();

//*********************Application Loop******************************************************************************************************************************************************************************************************************//


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


//*********************Cookies**********************************************************************************************************************************************************************************************************************************//



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowLocalhost3000");
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});


//*********************Controller Implemnting GET methods******************************************************************************************************************************************************************************************************//
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Login", action = "Login" });
app.MapControllerRoute(
    name: "logout",
    pattern: "Auth/logout",
    defaults: new { controller = "Auth", action = "logout" });
app.MapControllerRoute(
    name: "logup",
    pattern: "Logup",
    defaults: new { controller = "LogUp", action = "Logup" });
app.MapControllerRoute(
    name: "auth",
    pattern: "Auth/verify",
    defaults: new { controller = "Auth", action = "verify" });
app.MapControllerRoute(
    name: "logupSendRequest",
    pattern: "set/s",
    defaults: new { controller = "LogUp", action="LogupSubmitNormalUser"});
app.MapControllerRoute(
    name: "FoodList",
    pattern: "foodapi/Foods",
    defaults: new { controller = "FoodViewTable", action="GetAllFoods"});
app.MapControllerRoute (
    name: "GetFoodById",
    pattern: "foodapi/food/{id:int}",
    defaults: new {controller = "Food", action = "GetFoodDetails"}
);
app.MapControllerRoute(
    name: "NutrientsList",
    pattern: "nutrientapi/Nutrients",
    defaults: new { controller = "NutrientViewTable", action="GetAllNutrients"});
    
/*Static*/
app.MapControllerRoute(
    name: "StaticResult",
    pattern: "Static/StaticResult",
    defaults: new { controller = "Static", action="StaticResult"});

app.MapControllerRoute(
    name: "NormalUserProfile",
    pattern: "AdminUser/allnormalsser",
    defaults: new { controller = "AdminUser", action="allnormaluser"});
//*********************Controller Implemnting PUT methods******************************************************************************************************************************************************************************************************//
app.MapControllerRoute(
    name: "UpdateFoods",
    pattern: "foodapi/update/{id:int}",
    defaults: new { controller = "Food", action="UpdateFood"});
//*********************Controller Implemnting CREATE methods******************************************************************************************************************************************************************************************************//
app.MapControllerRoute(
    name: "createNewFood",
    pattern: "foodapi/create",
    defaults: new {controller = "Food", action = "CreateFood"}

);

//*********************End of Controller Implemnting******************************************************************************************************************************************************************************************************//


app.Run();
