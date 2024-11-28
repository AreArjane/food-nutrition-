


using System.Reflection.PortableExecutable;
using FoodRegistrationToolSub1.Models.datasets;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



[Route("foodapi")]
[ApiController]
public class FoodController : Controller {

    private readonly ApplicationDbContext _context;

    public FoodController(ApplicationDbContext context) { _context = context; }

    //GET the food.cshtml
    [HttpGet("")]
    public IActionResult FoodViewTable() { return View("/Views/Home/food.cshtml"); }

    [HttpGet("food/{id:int}")]
    public async Task<IActionResult> GetFoodDetails(int id) { 
/***************************************************************************************Validation*************************************************************************************************************************************************/
        if(id == null || id < 319874 ) { return BadRequest("Invalid ID data");}

        var food = await _context.Foods.FindAsync(id);
        
        if(food == null) { return NoContent();}
        
/************************************************************************************Find and manipulate the content****************************************************************************************************************************************************/
        var nutrients = _context.FoodNutrients.Where(fn => fn.FdcId == id).Select(fn => new { 
            fn.NutrientId,
            NutrientName = fn.Nutrient.Name,
            fn.DataPoint,
            fn.DerivationId,
            fn.Min,
            fn.Median,
            fn.Max,
            fn.Footnote,
            fn.MinYearAcquired
            
            }).ToListAsync();


            return Ok(new {
                foodId = food.FoodId,
                publicationDate = food.PublicationDate,
                description = food.Description,
                dataType = food.DataType,
                Nutrients = nutrients
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
    [HttpGet("Foods")]
    public async Task<IActionResult> GetAllFoods(int pagenumber, int pagesize, string? foodstartwith = null) {
        if (pagenumber <= 0 || pagesize <= 0){return BadRequest("Bad Request Paramteres should be greater than zero");}

         var foodQ = _context.Foods.Include(f => f.FoodCategory).AsQueryable();

            if(!string.IsNullOrEmpty(foodstartwith)) { 

                foodQ = foodQ.Where(fq => fq.Description != null && fq.Description.StartsWith(foodstartwith));
            }

            var totalFoods = await foodQ.CountAsync();
            var totalPage = totalFoods == 0 ? 1 : (int)Math.Ceiling(totalFoods / (double)pagesize);

            var foodss = await foodQ 
            .OrderBy(fq => fq.Description)
            .Skip((pagenumber - 1) * pagesize)
            .Take(pagesize)
            .Select(fq => new {

                fq.FoodId,
                fq.DataType,
                fq.Description,
                fq.PublicationDate,
                FoodCategory = fq.FoodCategory != null ? new {
                    fq.FoodCategory.Description,
                    fq.FoodCategory.Code
                } : null
                

            }).ToListAsync();

            var response = new {
                TotalCount = totalFoods,
                TotalPages = totalPage,
                CurrentPage = pagenumber,
                PageSize = pagesize,
                Data = foodss
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
    public async Task<IActionResult> DeleteFood(int id) {

        var min = await _context.Foods.MinAsync(f => f.FoodId); var max = await _context.Foods.MaxAsync(f => f.FoodId);
        
        if(id < min || id > max ) {return BadRequest("The following Id cannot be found it in database records");}

        var existingFood = await _context.Foods.FindAsync(id);

        if (existingFood == null) { return NotFound("Food not found.");}

        _context.Foods.Remove(existingFood);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Food deleted successfully." });
    }
    
}