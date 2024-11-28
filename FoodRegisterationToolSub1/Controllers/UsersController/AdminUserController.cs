//Admin Controller with Pagination impovment

using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controller responsible for handling administrative actions related to users, such as viewing profiles and managing normal users.
/// </summary>

[Route("AdminUser")]
[ApiController]


public class AdminUserController : Controller { 

 /// <summary>
    /// Instance of the application's database context for accessing user data.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdminUserController"/> class with the provided database context.
/// </summary>
/// <param name="context">Database context used for accessing user and administrative data.</param>    

    public AdminUserController(ApplicationDbContext context) { 
        _context = context;
    }


      /// <summary>
    /// Retrieves the profile data of a specific admin user if the user is authorized to view it.
    /// </summary>
    /// <param name="id">The ID of the admin user to retrieve profile information for.</param>
    /// <returns>An <see cref="IActionResult"/> containing the user's profile data, or an error response if unauthorized or not found.</returns>
    
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

    


      /// <summary>
    /// Retrieves a paginated list of all normal users, with optional filtering by name prefix.
    /// </summary>
    /// <param name="pagenumber">The current page number (default: 1).</param>
    /// <param name="pagesize">The number of users to include in each page (default: 20).</param>
    /// <param name="startwith">An optional filter to include users whose first names start with the specified string.</param>
    /// <returns>An <see cref="IActionResult"/> containing the paginated list of users and metadata.</returns>

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
