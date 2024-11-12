


using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

/// <summary>
/// Controller class to handle food-related data requests and retrieval of food details.
/// </summary>
public class FoodController : ControllerBase {

    private readonly ApplicationDbContext _context;

/// <summary>
    /// Initializes a new instance of the FoodController class, injecting the application database context.
    /// </summary>
    /// <param name="context">The application database context, providing access to food and nutrient data.</param>
    
    public FoodController(ApplicationDbContext context) {
        _context = context;
    }

    //GET

/// <summary>
    /// Retrieves details of a specific food item by its ID, including its nutrient information.
    /// </summary>
    /// <param name="id">The unique identifier of the food item to retrieve.</param>
    /// <returns>
    /// Returns an HTTP 200 OK response with food details and nutrients if found; otherwise, an HTTP 404 Not Found response.
    /// </returns>
    
    [HttpGet("{id}")]
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
