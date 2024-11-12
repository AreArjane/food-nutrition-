//Admin Controller with Pagination impovment

using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// API Controller for administrasjon av NormalUser, med støtte for søkefilter og paginering.
/// </summary>
public class AdminUserController : ControllerBase { 

    private readonly ApplicationDbContext _context;
   /// <summary>
    /// Initializes a new instance of the <see cref="AdminUserController"/> class, injecting the application database context.
    /// </summary>
    /// <param name="context">Database context for accessing NormalUser data.</param>
    public AdminUserController(ApplicationDbContext context) { 
        _context = context;
    }
    /// <summary>
    /// Retrieves a paginated list of normal users with optional filtering by name.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of users per page (default is 20).</param>
    /// <param name="NuserStartWith">Optional filter to retrieve users whose first name starts with the specified string.</param>
    /// <returns>
    /// An HTTP 200 OK response containing the paginated list of normal users, including metadata such as total count and total pages.
    /// </returns>
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
