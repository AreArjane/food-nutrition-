


using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class FoodController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public FoodController(ApplicationDbContext context) {
        _context = context;
    }

    //GET

    [HttpGet("food/{id}")]
    public IActionResult GetFoodDetails(int id) { 
        var food = _context.Foods.Find(id);
        if(food == null) {
            return NotFound();
        }

        var nutrients = _context.FoodNutrients
        .Where(fn => fn.FdcId == id)
        .Select(fn => new { 
            fn.NutrientId,
            NutrientName = fn.Nutrient.Name,
            fn.DataPoint,
            fn.DerivationId,
            fn.Min,
            fn.Median,
            fn.Max,
            fn.Footnote,
            fn.MinYearAcquired
            
          
            }).ToList();

            return Ok(new {
                FoodId = id,
                FoodName = food.Description,
                Nutrients = nutrients
            });
    }
}