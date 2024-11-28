


using System.Reflection.PortableExecutable;
using FoodRegistrationToolSub1.Models.datasets;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



[Route("foodnutrientapi")]
[ApiController]
public class FoodNutrientsController : Controller {

    private readonly ApplicationDbContext _context;

    public FoodNutrientsController(ApplicationDbContext context) { _context = context; }



    [HttpGet("foodnutrient/{id:int}")]
    public async Task<IActionResult> GetFoodNutrientDetails(int id) { 
        var foodnutrients = await _context.FoodNutrients.Include(fn => fn.Food).ThenInclude(f => f.FoodCategory)
        .Include(fn => fn.Nutrient)
        .FirstOrDefaultAsync(fn=> fn.Id == id);
        
        if(foodnutrients == null) { return NotFound($"FoodNutrients relation with the given {id} was not found");}

        var food = foodnutrients.Food;

        var foodnutrientList = await _context.FoodNutrients.Where(fn => fn.FdcId == food.FoodId).Include(fn => fn.Nutrient)
        .Select(fn => new {
            nutrientId = fn.NutrientId,
            nutrientName = fn.Nutrient.Name,
            unitName = fn.Nutrient.UnitName,
            nutrientnbr = fn.Nutrient.NutrientNbr,
        
        }).ToListAsync();

        return Ok(new {
                foodId = food.FoodId,
                publicationDate = food.PublicationDate,
                description = food.Description,
                dataType = food.DataType,
                category = food.FoodCategory != null ? new {
                    food.FoodCategory.Description,
                    food.FoodCategory.Code
                } : null,
                Nutrients = foodnutrientList
            });
    }
    /// <summary>
    /// Return all Foods registered in database with json format. It accept only with pagianation. PageSize represent 
    /// how many element it should return along with the pagenumber. The frontend application does not take this complexity of calculating 
    /// pagenumber along with the pagesizxe instead. It has a fixed value of pagesize equal to 10 or 20. \
    /// FoodStarwith is a filtering method. 
    /// </summary>
    /// <param name="pagenumber"></param>
    /// <param name="pagesize"></param>
    /// <param name="foodstartwith"></param>
    /// <returns></returns>
    [HttpGet("FoodNutrient")]
    public async Task<IActionResult> GetAllFoods(int pagenumber, int pagesize) {
        if (pagenumber <= 0 || pagesize <= 0){return BadRequest("Bad Request Paramteres should be greater than zero");}

         var foodQ = _context.FoodNutrients.Include(f => f.Food).ThenInclude(f => f.FoodCategory).Include(fn => fn.Nutrient).AsQueryable();


            var totalFoods = await foodQ.CountAsync();
            var totalPage = totalFoods == 0 ? 1 : (int)Math.Ceiling(totalFoods / (double)pagesize);

            var foodNutrientsList  = await foodQ 
            .OrderBy(fn => fn.Food.Description)
            .Skip((pagenumber - 1) * pagesize)
            .Take(pagesize)
            .Select(fq => new {

                foodId = fq.FdcId,
                description = fq.Food.Description,
                datatype = fq.Food.DataType,
                publicationdate = fq.Food.PublicationDate,
                foodcategory = fq.Food.FoodCategory != null ? new {fq.Food.FoodCategory.Description, fq.Food.FoodCategory.Code} : null,
                nutrientid = fq.NutrientId,
                nutrientname = fq.Nutrient.Name,
                nutrientunitname = fq.Nutrient.UnitName,
                nutrientnbr = fq.Nutrient.NutrientNbr,
                nutrientrank = fq.Nutrient.Rank
                

            }).ToListAsync();

            var response = new {
                TotalCount = totalFoods,
                TotalPages = totalPage,
                CurrentPage = pagenumber,
                PageSize = pagesize,
                Data = foodNutrientsList
            };

            return Ok(response);
    }
/// <summary>
/// Create new element of the Food with required description and dataType.
/// The category if its not given is set up to Null, uncatagorised food. 
/// It create new Id based on the max id value of the datasets models. Better soultion is to store the max value of the
/// id of the food table in such dictionary to speed up and give preformance to create Action, which it take time assoicated with the 
/// calculating max value. max value now 2717714.
/// </summary>
/// <param name="foodId"></param>
/// <param name="description"></param>
/// <param name="dataType"></param>
/// <param name="publicationDate"></param>
/// <param name="foodCategoryId"></param>
/// <returns></returns>

// POST: foodapi/create
    [HttpPost("create")]
    public async Task<IActionResult> CreateFood(
     [FromForm] string description, 
     [FromForm] string dataType, 
     [FromForm] string? publicationDate, 
     [FromForm] int? foodCategoryId) 
     {
        if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(dataType))
        {
            return BadRequest("Description and DataType are required.");
        }

        if(foodCategoryId.HasValue) {
            var categoryExist = await _context.FoodCategories.AnyAsync(fc => fc.Id == foodCategoryId.Value);

            if(!categoryExist) {
                Console.WriteLine($"The provided category does not exist {foodCategoryId}. Setting to NULL");
                foodCategoryId = null;
            } 
        }
        int newId;
        var maxId = await _context.Foods.MaxAsync(f => (int?)f.FoodId) ?? 2717714;
        newId = maxId + 1;

        var newFood = new Food
        {
            FoodId = newId,
            Description = description,
            DataType = dataType,
            PublicationDate = publicationDate,
            FoodCategoryId = foodCategoryId
        };

        await _context.Foods.AddAsync(newFood);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFoodDetails), new { id = newFood.FoodId }, newFood);
    }
    /// <summary>
    /// Accept only data given by form-data body with PUT API. 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="dataType"></param>
    /// <param name="publicationDate"></param>
    /// <returns>Nofouund if there is not match with ID in data registered or OK if succeded</returns>
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateFood(int id, [FromForm] string? description, [FromForm] string? dataType, [FromForm] string? publicationDate)
    {
        var existingFood = await _context.Foods.FindAsync(id);
        if (existingFood == null)
        {
            return NotFound("Food not found.");
        }

        if (!string.IsNullOrEmpty(description)) existingFood.Description = description;
        if (!string.IsNullOrEmpty(dataType)) existingFood.DataType = dataType;
        if (!string.IsNullOrEmpty(publicationDate)) existingFood.PublicationDate = publicationDate;

       
        await _context.SaveChangesAsync();

        return Ok(existingFood);
    }
    /// <summary>
    /// DELETE the record of Food given by id.
    /// FoodNutrients as the many-to-many relationship model with FOOD has models.CASCADE delete method.
    /// To this order there is not need to check against the ID of the food in FoodNutrient table.
    /// </summary>
    /// <example>var existingFood = await _context.Foods.Include(f => f.FoodNutrients).FirstOrDefaultAsync(f => f.FoodId == id);</example>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteFood(int id)
    {
        var existingFood = await _context.Foods.FindAsync(id);
        if (existingFood == null)
        {
            return NotFound("Food not found.");
        }

        _context.Foods.Remove(existingFood);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Food deleted successfully." });
    }
    
}