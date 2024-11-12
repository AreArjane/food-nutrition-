
using FoodRegistrationToolSub1.Models.datasets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
/// <summary>
/// Controller for handling meal-related data retrieval and details.
/// </summary>
public class MealsController : ControllerBase {

    private readonly ApplicationDbContext _context;
   /// <summary>
    /// Initializes a new instance of the MealsController class, injecting the application database context.
    /// </summary>
    /// <param name="context">The application database context, providing access to meal and food-related data.</param>
    
    public MealsController(ApplicationDbContext context) {
        _context = context;
    }
 /// <summary>
    /// Retrieves details of a specific meal by its ID, including related food items and their nutrient information.
    /// </summary>
    /// <param name="id">The unique identifier of the meal to retrieve.</param>
    /// <returns>
    /// Returns an HTTP 200 OK response with the meal details, including foods and nutrients if found; otherwise, an HTTP 404 Not Found response.
    /// </returns>
    //Get 
    [HttpGet("{id}/Details")]
    public IActionResult GetMealsDetails(int id) {

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
