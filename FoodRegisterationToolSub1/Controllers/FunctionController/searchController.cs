using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class searchController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public searchController(ApplicationDbContext context) { 

        _context = context;
    }

    // GET api/Search?query={name}

    [HttpGet]
    public IActionResult Search(string query) { 

        if(string.IsNullOrEmpty(query)) {
            return BadRequest("Search query is required");
        }

        var foods = _context.Foods
        .Where(f => f.Description.Contains(query.ToLower()))
        .Select(f => new { 
            f.Description,
            f.DataType,
            f.PublicationDate,

        }).Take(10).ToList();

        var meals = _context.Meal 
        .Where(m => m.Name.Contains(query.ToLower()))
        .Select(m => new { 
            m.Name
        }).ToList();

        return Ok(new {Foods = foods, Meal = meals});
    }
}