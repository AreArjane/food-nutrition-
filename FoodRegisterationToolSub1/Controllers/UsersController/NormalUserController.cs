
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]


public class NormalUserController : Controller {

    private readonly ApplicationDbContext _context;

    public NormalUserController(ApplicationDbContext context) { 
        _context = context;
    }

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