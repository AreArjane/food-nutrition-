

using System.Security.Claims;
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
[Route("Auth")]
[ApiController]
public class AuthController  : Controller  { 

    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<NormalUser> _passwordHasher;
    private readonly PasswordHasher<SuperUser> _passwordHasher_s;
     private readonly PasswordHasher<PendingSuperUser> _passwordHasher_ps;
    

    public AuthController(ApplicationDbContext context) { 
        _context = context;
        _passwordHasher = new PasswordHasher<NormalUser>();
        _passwordHasher_s = new PasswordHasher<SuperUser>();
        _passwordHasher_ps = new PasswordHasher<PendingSuperUser>();
    
    }

    [HttpPost("verify")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password, [FromForm] UserType userType) { 
        Console.WriteLine($"Received request with Email: {email} with password : {password} with the usertype {userType}");
    



        User user = null;
        PasswordVerificationResult passwordVerification = PasswordVerificationResult.Failed;

        switch(userType) 
        {
            case UserType.NormalUser:
            
            user = _context.NormalUsers.FirstOrDefault(nu => nu.Email == email && nu.UserType == userType);
            
            if(user != null) {  passwordVerification = _passwordHasher.VerifyHashedPassword((NormalUser)user, user.Password, password); }               break;

            case UserType.PendingSuperUser:

            user = _context.PendingSuperUser.FirstOrDefault(psu => psu.Email == email && psu.UserType == userType);

            if(user != null) { passwordVerification = _passwordHasher_ps.VerifyHashedPassword((PendingSuperUser)user, user.Password, password); }       break;

            case UserType.SuperUser:

            user = _context.SuperUsers.FirstOrDefault(su => su.Email == email && su.UserType == userType);

            if(user != null) { passwordVerification = _passwordHasher_s.VerifyHashedPassword((SuperUser)user, user.Password, password); }               break;


            default:
                return Json(new {success = false, errorMessage = "Unknow user type. Please check your user type and try again."});


        }
        
       

  if (user == null || passwordVerification != PasswordVerificationResult.Success)
{
    ModelState.AddModelError("", "Invalid Email or Password");
    return Json(new { success = false, errorMessage = "Invalid Email or Password" });
}





       var claims = new List<Claim> {
        
        new Claim("UserId", user.UserId.ToString()),
        new Claim(ClaimTypes.Role, user.UserType.ToString())
        
        };
        
     
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        
      
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        HttpContext.Session.SetInt32("UserID", user.UserId);
        string redirectUrl;

       if (user.UserType == UserType.NormalUser)
        {
            redirectUrl = Url.Action("GetUserProfileView", "NormalUser", new { id = user.UserId });
        }
        else if (user.UserType == UserType.SuperUser)
        {
            redirectUrl = Url.Action("GetUserProfileView", "SuperUser", new { id = user.UserId});
        }
        else if (user.UserType == UserType.AdminUser)
        {
            redirectUrl = Url.Action("GetUserProfile", "AdminUser", new {id = user.UserId});
        }
        
        else { 
            return Json(new {success = false, errorMessage = "Unknow userType try and check your usertype again"});
        }

        return Json(new {success = true, redirectUrl = redirectUrl });




    }
    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() {

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        HttpContext.Session.Clear();

        return Json(new {success = true, redirectUrl = Url.Action("Login", "Login")});
    }




    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        
        return View();
    }
}