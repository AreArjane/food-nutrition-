
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Controller responsible for managing normal user profiles, including retrieving profile data and rendering profile views.
/// </summary>

[Route("api/[controller]")]
[ApiController]


public class NormalUserController : Controller {
/// <summary>
    /// Instance of the application's database context for accessing normal user data.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="NormalUserController"/> class with the provided database context.
/// </summary>
/// <param name="context">Database context used for accessing user data.</param>
    public NormalUserController(ApplicationDbContext context) { 
        _context = context;
    }
 /// <summary>
    /// Retrieves the profile data of a specific normal user based on their ID.
/// </summary>
/// <param name="id">The ID of the normal user to retrieve profile information for.</param>
/// <returns>An <see cref="IActionResult"/> containing the user's profile data, or an error response if not found or access is forbidden.</returns>
    [HttpGet("{id}/profile")]

    public IActionResult GetUserProfile(int id) {

        var normal_user = _context.NormalUsers.Find(id);

        if(normal_user == null) { 
            
            return NotFound();

        } 
        
        if (normal_user.UserType != UserType.NormalUser) {
            
            return Forbid();
        } 

            var profile_data_NU = new {

                normal_user.FirstName,
                normal_user.PhoneNr,
                normal_user.LastName,
                normal_user.Email,
                normal_user.HomeAddress,
                normal_user.PostalCode


            };
        

        return Ok(profile_data_NU);
    }
 /// <summary>
    /// Retrieves and renders the profile view for a specific normal user.
/// </summary>
/// <param name="id">The ID of the normal user to retrieve and render profile information for.</param>
/// <returns>An <see cref="IActionResult"/> that renders the profile view or an error response if access is forbidden or user is not found.</returns>
    [HttpGet("{id}/profile/view")]
    public IActionResult GetUserProfileView(int id)
    {
        var loggedInUserId = HttpContext.User.FindFirst("UserId")?.Value;

        if (loggedInUserId == null || int.Parse(loggedInUserId) != id)
        {
            return Forbid();
        }

        var normal_user = _context.NormalUsers.Find(id);

        if (normal_user == null)
        {
            return NotFound();
        }

        if (normal_user.UserType != UserType.NormalUser)
        {
            return Forbid();
        }

        var profileData = new
        {
            normal_user.FirstName,
            normal_user.PhoneNr,
            normal_user.LastName,
            normal_user.Email,
            normal_user.UserType,
            id
        };

        return View("/Views/Profile/NormalUser.cshtml", profileData);
    }



}
