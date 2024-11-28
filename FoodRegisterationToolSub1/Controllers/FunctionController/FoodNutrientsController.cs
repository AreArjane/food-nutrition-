


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
    /// <summary>
    /// Return a single FoodNutrient relationship between food and nutrients with the give ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

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
    /// Return all FoodsNutrients registered in database with json format. It accept only with pagianation. PageSize represent 
    /// how many element it should return along with the pagenumber. The frontend application does not take this complexity of calculating 
    /// pagenumber along with the pagesizxe instead. It has a fixed value of pagesize equal to 10 or 20. \
    /// </summary>
    /// <param name="pagenumber"></param>
    /// <param name="pagesize"></param>
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
/// Create new relation for FOOD and Nutrient. 
/// At the begning it validate each description with non/null and each ID with the minimum value of datatbase record. Its fixed here (valvulated with SQL)
/// It call the validation on numeric and alfabetical input.
/// It check if the food and nutrient given exist in the Food and Nutrient table, if not it return a BadRequest with proposal to create a record for each before submitting.
/// When it success and both Food and Nutrient exist it add the new relationship. It good practise to take the data as the [FormBody] instead of single input.
/// This make it easier in the front end developing, while the datasets was not modified and the table relationship is breaking a many-to-many relationship between Food and Nutrient.
/// Therefore this function relay on such architecture to check boot Food and Nutrient existence. 
/// 
/// This is manipulated with superuser after adding new Food and nutrient. 
/// In Frontend we as we can call several endpoint and make the request more seemenless by callning the creating of Food and Nutrient before FoodNutrieent relationship. 
/// </summary>
/// <param name="description"></param>
/// <param name="nutrient"></param>
/// <param name="foodid"></param>
/// <param name="min"></param>
/// <param name="median"></param>
/// <param name="max"></param>
/// <param name="footnote"></param>
/// <param name="datapoint"></param>
/// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> CreateFoodNutrient(
     [FromForm] string description, 
     [FromForm] int nutrient,
     [FromForm] int foodid,
     [FromForm] string min,
     [FromForm] string median,
     [FromForm] string max,
     [FromForm] string footnote,
     [FromForm] string datapoint)
     {

/********************************************************************************Validation******************************************************************************************************/
        
        if(string.IsNullOrEmpty(description) ||  nutrient < 999 || foodid < 319874) {return BadRequest("Invalid assigned data request. All fields are required");}

        
        var numericvalues = ValidatorSpes.IsOnlyNumber(min, median, max, datapoint);

        if(numericvalues.Contains(false)) { return BadRequest("The provided vlaue of min, median and max should only be numerical value");}

        var alfabiticalvalue = ValidatorSpes.ValidateNorwegianAlphabet(footnote, description);

        if(alfabiticalvalue.Contains(false)) { return BadRequest("The provided data of description and footnote should contain only alfabetical value.");}

/****************************************************************Check existence*************************************************************************************************************************************/
        
        var foodExist = await _context.Foods.FirstOrDefaultAsync(f => f.Description.ToUpper().Contains(description.ToUpper(), StringComparison.OrdinalIgnoreCase));
        if(foodExist == null) { 
            return NotFound($"Food with description containting {description} not found. Please create the food in the food creating form first");}

        var nutrientexist = await _context.Nutrients.FirstOrDefaultAsync(n => n.Id == nutrient);
        if(nutrientexist == null) { 
            return NotFound($"Nutrient with ID  '{nutrient}' not found. Please create the nutrient record first");
        }

        var foodnutrientexist = await _context.FoodNutrients.FirstOrDefaultAsync(fn => fn.FdcId == foodid && fn.NutrientId == nutrient);
        if(foodnutrientexist == null) { 
            //calcualting new Id for FoodNutrient record
            int newId;
            var maxId = await _context.FoodNutrients.MaxAsync(f => (int?)f.Id) ?? 34494304;
            newId = maxId + 1;
            
            var newFoodNutrients = new FoodNutrient { 
                Id              = newId,
                FdcId           = foodExist.FoodId,
                NutrientId      = nutrientexist.Id,
                DataPoint       = datapoint,
                Min             = min,
                Median          = median,
                Max             = max,
                Footnote        = footnote


            };
            await _context.FoodNutrients.AddAsync(newFoodNutrients);
        }
        else { 
            foodnutrientexist.DataPoint = datapoint;
            foodnutrientexist.Min = min;
        }

    await _context.SaveChangesAsync();

        return Ok(new {message = "Food with nuterient relation been registered successfully"});
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
        var min = await _context.FoodNutrients.MinAsync(fn => fn.Id);
        var max = await _context.FoodNutrients.MaxAsync(fn => fn.Id);
        
        if(id < min || id > max) { return BadRequest("The FoodNutrient Id cannot be found it in the database records");}

        var existingFood = await _context.FoodNutrients.FindAsync(id);

        if (existingFood == null) { return NotFound("Food not found.");}

        _context.FoodNutrients.Remove(existingFood);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "FoodNutrients relationship deleted successfully." });
    }
    
}