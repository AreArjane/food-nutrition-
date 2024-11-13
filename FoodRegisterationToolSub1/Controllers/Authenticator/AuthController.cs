

using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Controller for autentisering av brukere i applikasjonen.
/// Håndterer pålogging og brukersesjoner.
/// </summary>
public class AuthController  : Controller  { 

    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<NormalUser> _passwordHasher;
    /// <summary>
    /// Initialiserer en ny instans av <see cref="AuthController"/>-klassen.
    /// </summary>
    /// <param name="context">Databasekonteksten for å få tilgang til brukerinformasjon.</param>
    public AuthController(ApplicationDbContext context) { 
        _context = context;
        _passwordHasher = new PasswordHasher<NormalUser>();
    
    }
    /// <summary>
    /// Utfører pålogging for brukere basert på e-post, passord og brukertype.
    /// Verifiserer brukerens legitimasjon og oppretter en sesjon ved vellykket autentisering.
    /// </summary>
    /// <param name="email">Brukerens e-postadresse.</param>
    /// <param name="password">Brukerens passord.</param>
    /// <param name="userType">Typen brukerkonto som logges inn (f.eks. NormalUser).</param>
    /// <returns>
    /// En <see cref="IActionResult"/> som representerer visningen av brukerens profilside
    /// ved vellykket pålogging, eller innloggingssiden ved mislykket forsøk.
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
