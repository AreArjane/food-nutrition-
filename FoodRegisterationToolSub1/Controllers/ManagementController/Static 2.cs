using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Route("Static")]
[ApiController]
public class StaticController : Controller { 

    private readonly ApplicationDbContext _context;

    public StaticController(ApplicationDbContext context) { 
        _context = context;
    }



    [HttpGet("StaticResult")]
    public async Task<IActionResult> StaticResult() { 

        var staticCounter = new { 
            Users = await _context.Users.CountAsync(),
            Foods = await _context.Foods.CountAsync(),
            Meals = await _context.Meal.CountAsync(),
            FoodCategories = await _context.FoodCategories.CountAsync()
        };
        

        return Ok(new {staticCounter});

    }
}