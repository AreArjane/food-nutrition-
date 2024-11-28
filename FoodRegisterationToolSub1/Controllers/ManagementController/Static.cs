using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Route("Static")]
[ApiController]

public class StaticController : Controller { 

    private readonly ApplicationDbContext _context;

    public StaticController(ApplicationDbContext context) { 
        _context = context;
    }
/// <summary>
/// Static controller GET method, return the static data, a counting method to the records registered in the databases.
/// </summary>
/// <returns></returns>
    [HttpGet("StaticResult")]
    public async Task<IActionResult> StaticResult() { 

        var staticCounter = new { 
            Users           = await _context.Users.CountAsync(),
            Foods           = await _context.Foods.CountAsync(),
            Meals           = await _context.Meal.CountAsync(),
            FoodCategories  = await _context.FoodCategories.CountAsync(),
            FoodNutriens    = await _context.FoodNutrients.CountAsync()
        };
        

        return Ok(new {staticCounter});

    }
}
