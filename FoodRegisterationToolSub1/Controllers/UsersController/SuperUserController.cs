
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Route("api/[controller]")]
[ApiController]


public class SuperUserController : Controller {

    private readonly ApplicationDbContext _context;

    public SuperUserController(ApplicationDbContext context) { 
        _context = context;
    }

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