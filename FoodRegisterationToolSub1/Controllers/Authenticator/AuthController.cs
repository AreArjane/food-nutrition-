

using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller class responsible for managing authentication operations 
/// such as login functionality for different user types.
/// </summary>
public class AuthController  : Controller  { 

    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<NormalUser> _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the AuthController class with database context
    /// and password hashing utilities for user authentication.
    /// </summary>
    /// <param name="context">The application database context for accessing user data.</param>
    public AuthController(ApplicationDbContext context) { 
        _context = context;
        _passwordHasher = new PasswordHasher<NormalUser>();
    
    }
 /// <summary>
    /// Handles user login requests. Validates user credentials and manages session setup.
    /// </summary>
    /// <param name="email">The email address of the user attempting to log in.</param>
    /// <param name="password">The password provided by the user for authentication.</param>
    /// <param name="userType">The type of user (e.g., NormalUser) logging in.</param>
    /// <returns>
    /// Redirects to the user's profile page if login is successful, or back to login page
    /// with an error if login fails.
    /// </returns>
    
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
