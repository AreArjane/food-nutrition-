using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class searchController : ControllerBase {
    private readonly ApplicationDbContext _context;
    public searchController(ApplicationDbContext context) { _context = context; }
/// <summary>
/// Search method for Food with nutrient accessed by public.
/// </summary>
/// <param name="query"></param>
/// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Search(string query) { 

        if(string.IsNullOrEmpty(query)) { return BadRequest("Search query is required"); }

        var foods = await _context.Foods.Where(f => f.Description.Contains(query.ToLower())).Select(f => new { 
            f.Description,
            f.DataType,
            f.PublicationDate, }).Take(10).ToListAsync();

        var meals = await _context.Meal.Where(m => m.Name.Contains(query.ToLower())).Select(m => new {  m.Name }).ToListAsync();

        return Ok(new {Foods = foods, Meal = meals});
    }
}
