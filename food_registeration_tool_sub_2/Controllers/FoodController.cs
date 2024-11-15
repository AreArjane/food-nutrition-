[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FoodController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFoods()
    {
        var foods = await _context.FoodItems.ToListAsync();
        return Ok(foods);
    }

    [HttpPost]
    public async Task<IActionResult> AddFood([FromBody] FoodItem foodItem)
    {
        if (foodItem == null)
        {
            return BadRequest("Invalid data.");
        }

        _context.FoodItems.Add(foodItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFoods), new { id = foodItem.Id }, foodItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFood(int id)
    {
        var food = await _context.FoodItems.FindAsync(id);
        if (food == null)
        {
            return NotFound();
        }

        _context.FoodItems.Remove(food);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
