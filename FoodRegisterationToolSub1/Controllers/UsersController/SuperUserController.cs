
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]


public class SuperUserController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public SuperUserController(ApplicationDbContext context) { 
        _context = context;
    }

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