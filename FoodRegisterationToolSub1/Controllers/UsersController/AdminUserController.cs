//Admin Controller with Pagination impovment

using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class AdminUserController : ControllerBase { 

    private readonly ApplicationDbContext _context;

    public AdminUserController(ApplicationDbContext context) { 
        _context = context;
    }

    [HttpGet("{id}/profile")]
    public IActionResult GetUserProfile(int id)
    {
        // Ensure the user is logged in by checking the session
        var sessionUserId = HttpContext.Session.GetInt32("UserID");

        if (sessionUserId == null || sessionUserId != id)
        {
            return Unauthorized();
        }

        // Retrieve the AdminUser's profile information
        var adminUser = _context.AdminUser.Find(id);
        if (adminUser == null)
        {
            return NotFound();
        }

        // Create a profile data response for the admin user
        var profileData = new
        {
            adminUser.FirstName,
            adminUser.PhoneNr,
            adminUser.NationalIdenityNumber,
            adminUser.OfficeAddress
        };

        return Ok(profileData);
    }

    




    [HttpGet("adm/allNormalUser")]

    public IActionResult GetAllNormalUser(

        //<<required>> access controll

        int pageNumber = 1, 
        int pageSize = 20, 
        string NuserStartWith = null) {

            var userQ = _context.NormalUsers.AsQueryable();

            if(!string.IsNullOrEmpty(NuserStartWith)) { 

                userQ = userQ.Where(nu => nu.FirstName.StartsWith(NuserStartWith));
            }

            var totalUsers = userQ.Count();
            var totalPage = (int)Math.Ceiling(totalUsers / (double)pageSize);

            var normal_user = userQ 
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(nu => new {

                nu.FirstName,
                nu.PhoneNr,
                nu.LastName,
                nu.Email,
                nu.HomeAddress,
                nu.PostalCode

            }).ToList();

            var response = new {
                TotalCount = totalUsers,
                TotalPages = totalPage,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Data = normal_user
            };

            return Ok(response);


        }


}