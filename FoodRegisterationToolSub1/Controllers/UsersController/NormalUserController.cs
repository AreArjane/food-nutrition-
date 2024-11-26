
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// API Controller for håndtering av normalbrukerdata, inkludert henting av brukerprofilinformasjon.
/// </summary>

public class NormalUserController : ControllerBase {

    private readonly ApplicationDbContext _context;
    /// <summary>
    /// Initializes a new instance of the <see cref="NormalUserController"/> class, injecting the application database context.
    /// </summary>
    /// <param name="context">Database context for accessing normal user-related data.</param>
    public NormalUserController(ApplicationDbContext context) { 
        _context = context;
    }
     /// <summary>
    /// Retrieves the profile data of a normal user by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the normal user.</param>
    /// <returns>
    /// An HTTP 200 OK response containing the normal user's profile data if found and valid; 
    /// an HTTP 404 Not Found response if the user does not exist; 
    /// or an HTTP 403 Forbidden response if the user type is not authorized.
    /// </returns>
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



}
