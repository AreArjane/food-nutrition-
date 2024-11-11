

using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AuthController  : Controller  { 

    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<NormalUser> _passwordHasher;

    public AuthController(ApplicationDbContext context) { 
        _context = context;
        _passwordHasher = new PasswordHasher<NormalUser>();
    
    }

    [HttpPost]
    public IActionResult Login(string email, string password, string userType) { 

        var user = _context.NormalUsers.FirstOrDefault(nu => nu.Email == email && nu.UserType.ToString() == userType);

        if(user == null) { 
            ModelState.AddModelError("", "Invalid Email or Password");
            return View();
        }

        var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        if (passwordVerification != PasswordVerificationResult.Success) {

            ModelState.AddModelError("", "Invalid Password or Email");
        }

        HttpContext.Session.SetInt32("UserID", user.UserId);

        if(userType == user.UserType.ToString())  { 

            return RedirectToAction("GetUserProfile", "NormalUser", new {id = user.UserId});
        }

        return RedirectToAction("Login", "Login");




    }
}