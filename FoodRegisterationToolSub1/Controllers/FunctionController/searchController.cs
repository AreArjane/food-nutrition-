using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
/// <summary>
/// API Controller for handling search queries for foods and meals.
/// </summary>
public class searchController : ControllerBase {

    private readonly ApplicationDbContext _context;

/// <summary>
    /// Initializes a new instance of the searchController class, injecting the application database context.
    /// </summary>
    /// <param name="context">The application database context, providing access to food and meal data.</param>
    public searchController(ApplicationDbContext context) { 

        _context = context;
    }

    // GET api/Search?query={name}
    /// <summary>
    /// Searches for foods and meals based on a specified query string.
    /// </summary>
    /// <param name="query">The search query string used to filter foods and meals by name.</param>
    /// <returns>
    /// Returns an HTTP 200 OK response with search results if found, or an HTTP 400 Bad Request response if the query is empty or null.
    /// </returns>
    [HttpGet]
    public IActionResult Search(string query) { 

        if(string.IsNullOrEmpty(query)) {
            return BadRequest("Search query is required");
        }

        var foods = _context.Foods
        .Where(f => f.Description.Contains(query))
        .Select(f => new { 
            f.Description,
            f.DataType,
            f.PublicationDate,

        }).ToList();

        var meals = _context.Meal 
        .Where(m => m.Name.Contains(query))
        .Select(m => new { 
            m.Name
        }).ToList();

        return Ok(new {Foods = foods, Meal = meals});
    }
}
