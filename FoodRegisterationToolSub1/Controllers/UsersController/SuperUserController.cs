
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
/// <summary>
/// Controller responsible for managing super user profiles, including retrieving profile data and rendering profile views.
/// </summary>
[Route("api/[controller]")]
[ApiController]


public class SuperUserController : Controller {
/// <summary>
    /// Instance of the application's database context for accessing super user data.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SuperUserController"/> class with the provided database context.
/// </summary>
/// <param name="context">Database context used for accessing super user data.</param>
    public SuperUserController(ApplicationDbContext context) { 
        _context = context;
    }
   /// <summary>
    /// Retrieves the profile data of a specific super user based on their ID, ensuring the requesting user is authorized.
/// </summary>
/// <param name="id">The ID of the super user to retrieve profile information for.</param>
/// <returns>An <see cref="IActionResult"/> containing the user's profile data, or an error response if unauthorized or not found.</returns>
    [HttpGet("{id}/profile")]

    public IActionResult GetUserProfile(int id) {

        var loggedInUserId = HttpContext.User.FindFirst("UserId")?.Value;
        
        if(loggedInUserId == null || int.Parse(loggedInUserId) != id) {
            return Forbid();
        }
        var super_user = _context.SuperUsers.Find(id);

        if(super_user == null) { 
            
            return NotFound();

        } 
        
        if (super_user.UserType != UserType.SuperUser) {
            
            return Forbid();
        } 

            var profile_data_SU = new {

               super_user.FirstName,
               super_user.PhoneNr,
               super_user.DateOfBirth,
               super_user.Email,
               super_user.UserType
                
            };
        

        return Ok(profile_data_SU);
    }

  /// <summary>
    /// Retrieves and renders the profile view for a specific super user, ensuring the requesting user is authorized.
/// </summary>
/// <param name="id">The ID of the super user to retrieve and render profile information for.</param>
/// <returns>An <see cref="IActionResult"/> that renders the profile view or an error response if unauthorized or not found.</returns>
    [HttpGet("{id}/profile/view")]
    public IActionResult GetUserProfileView(int id)
    {
        var loggedInUserId = HttpContext.User.FindFirst("UserId")?.Value;

        if (loggedInUserId == null || int.Parse(loggedInUserId) != id)
        {
            return Forbid();
        }

        var super_user = _context.SuperUsers.Find(id);

        if (super_user == null)
        {
            return NotFound();
        }

        if (super_user.UserType != UserType.SuperUser)
        {
            return Forbid();
        }

        var profileData = new
        {
            super_user.FirstName,
            super_user.PhoneNr,
            super_user.DateOfBirth,
            super_user.Email,
            super_user.UserType,
            id
        };

        return View("/Views/Profile/SuperUser.cshtml", profileData);
    }



}
