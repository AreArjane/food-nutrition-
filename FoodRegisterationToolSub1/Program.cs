
using Microsoft.Extensions.DependencyInjection;
using FoodRegisterationToolSub1;
using Microsoft.AspNetCore.Identity;
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

//*********************Rate Limit Configuration**********************************//
    /**    builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
        builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        builder.Services.AddHttpContextAccessor();**/
//*****************************************************Cookies Configuration*****************************************************************************/

//Database configuration
DataBaseConfiguration.ConfigurationDatabase(builder.Services);
//Import dataset to database
builder.Services.AddScoped<DataSetImporter>();




//***********************Container View********************************//

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



/***********************Building the app ***************************************************************************************************************/

var app = builder.Build();


//***************************************************************************************************************************************************//
/*using (var scope = app.Services.CreateAsyncScope()) {
    var services = scope.ServiceProvider;
    try {
        var dataImporter = services.GetRequiredService<DataSetImporter>();
        dataImporter.ImportData();
        Console.WriteLine("Data imported successfully");
    }

    catch (Exception e) {
        Console.WriteLine($"An error occured with importing dataset into database {e.Message}");
    }
}*/

//*********************Rate Limit Implementing**********************************//

    //app.UseIpRateLimiting();

//*********************Application Loop**********************************//


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


//*********************Cookies**********************************//



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();



//*********************Controller Implemnting**********************************************************************************************************************//
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Login", action = "Login" });

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

//*********************End of Controller Implemnting**********************************************************************************************************************//


app.Run();
