
using FoodRegistrationToolSub1.Models.datasets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsible for handling meal-related API operations, such as fetching meal details.
/// </summary>

[Route("apimeals/[controller]")]
[ApiController]
public class MealsController : ControllerBase {
   /// <summary>
    /// Instance of the application's database context for accessing meal-related data.
    /// </summary>
    private readonly ApplicationDbContext _context;
     /// <summary>
    /// Initializes a new instance of the <see cref="MealsController"/> class with the database context.
    /// </summary>
    /// <param name="context">Database context used for accessing meal data.</param>
    public MealsController(ApplicationDbContext context) {
        _context = context;
    }

  /// <summary>
    /// Retrieves detailed information about a specific meal, including its foods and nutrient composition.
    /// </summary>
    /// <param name="id">The ID of the meal to fetch details for.</param>
    /// <returns>An <see cref="IActionResult"/> containing meal details, or a 404 error if the meal is not found.</returns>
     
    //Get 
    [HttpGet("meal/{id}/Details")]
    public IActionResult GetMealsDetails(int id) {
    
      // Implementation for fetching and returning meal details.
        var meal = _context.Meal.Find(id);
        if (meal == null) {
            return NotFound();
        }

        var food = _context.MealFood
        .Where(mf => mf.MealFoodId == id)
        .Select(mf => new {
            mf.FoodId,
            FoodName = mf.Food.Description
        }).ToList();


        var nutrients = _context.FoodNutrients
        .Where(fn => food.Select(f => f.FoodId).Contains(fn.FdcId))
        .GroupBy(fn => fn.NutrientId)
        .Select(g => new {
            NutrientId = g.Key,
            NutrientName = g.First().Nutrient.Name,
            TotalAmount = g.Sum(fn => fn.Amount)

            
        }).ToList();

        return Ok( new {
            MealId = id,
            MealName = meal.Name,
            Foods = food,
            Nutrients = nutrients
        });
    }
}
