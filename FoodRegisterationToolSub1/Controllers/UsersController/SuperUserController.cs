
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// API Controller for håndtering av superbrukerdata, inkludert tilgang til profilinformasjon.
/// </summary>

public class SuperUserController : ControllerBase {

    private readonly ApplicationDbContext _context;
    /// <summary>
    /// Initializes a new instance of the <see cref="SuperUserController"/> class, injecting the application database context.
    /// </summary>
    /// <param name="context">Database context for accessing super user-related data.</param>
    public SuperUserController(ApplicationDbContext context) { 
        _context = context;
    }
    /// <summary>
    /// Retrieves the profile data of a super user by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the super user.</param>
    /// <returns>
    /// An HTTP 200 OK response containing the super user's profile data if found and valid; 
    /// an HTTP 404 Not Found response if the user does not exist; 
    /// or an HTTP 403 Forbidden response if the user type is not authorized.
    /// </returns>
    [HttpGet("{id}/profile")]

    public IActionResult GetUserProfile(int id) {

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
                
            };
        

        return Ok(profile_data_SU);
    }



}
