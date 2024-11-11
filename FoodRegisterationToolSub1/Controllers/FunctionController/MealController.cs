
using FoodRegistrationToolSub1.Models.datasets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MealsController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public MealsController(ApplicationDbContext context) {
        _context = context;
    }

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