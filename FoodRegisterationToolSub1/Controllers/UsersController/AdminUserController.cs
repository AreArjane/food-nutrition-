//Admin Controller with Pagination impovment

using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("AdminUser")]
[ApiController]
public class AdminUserController : Controller { 

    private readonly ApplicationDbContext _context;

    public AdminUserController(ApplicationDbContext context) { 
        _context = context;
    }

    [HttpGet("{id}/profile")]
    public IActionResult GetUserProfile(int id)
    {
        
        var sessionUserId = HttpContext.Session.GetInt32("UserID");

        if (sessionUserId == null || sessionUserId != id)
        {
            return Unauthorized();
        }

       
        var adminUser = _context.AdminUser.Find(id);
        if (adminUser == null)
        {
            return NotFound();
        }

        
        var profileData = new
        {
            adminUser.FirstName,
            adminUser.PhoneNr,
            adminUser.NationalIdenityNumber,
            adminUser.OfficeAddress
        };

        return Ok(profileData);
    }

    




    [HttpGet("allnormaluser")]
    public async Task<IActionResult> GetAllNormalUser(
        int pagenumber = 1, 
        int pagesize = 20, 
        string startwith = null) {

            var userQ = _context.NormalUsers.AsNoTracking();

            if(!string.IsNullOrEmpty(startwith)) { 

                userQ = userQ.Where(nu => nu.FirstName.StartsWith(startwith));
            }

            var totalUsers = await userQ.CountAsync();
            var totalPage = totalUsers == 0 ? 1 : (int)Math.Ceiling(totalUsers / (double)pagesize);

            var normal_user = await userQ 
            .Skip((pagenumber - 1) * pagesize)
            .Take(pagesize)
            .Select(nu => new {

                nu.FirstName,
                nu.PhoneNr,
                nu.LastName,
                nu.Email,
                nu.HomeAddress,
                nu.PostalCode,
                nu.UserId,
                nu.UserType

            }).ToListAsync();

            var response = new {
                totalCount = totalUsers,
                totalPages = totalPage,
                currentPage = pagenumber,
                pageSize = pagesize,
                data = normal_user
            };

            return Ok(response);


        }


}