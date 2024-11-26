
using FoodRegisterationToolSub1.Models.datasets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("foodcategory")]
[ApiController]

///<summary>
///Class <c>FoodCategoryController</c> a repository controller fo the FoodCategory Models With CRUD operation follow API architecture.
///<example>
///Router all method should have a router name followed to the mehtod link /foodcategory/*
///GET      /FoodCategories                     -> return all FoodCategories in the models.
///GET      /foodcategories/{id:int}            -> return the specific categories by its Id given.
///POST     /foodcategoires/new                 -> create a new foodcategories with id, code and description
///PUT      /foodcategories/update/{id:int}     -> update the specified foodcategories by id
///DELETE   /foodcategories/delete/{id:int}     -> delete the specified foodcategories by id
///</example>
///</summary>

public class FoodCategoryController : Controller { 

    private readonly ApplicationDbContext _context;

    public FoodCategoryController(ApplicationDbContext context) {
        _context = context;
    }

    [HttpGet("foodcategories/{id:int}")]
    public async Task<IActionResult> GetFoodCategoriesDetails(int id) { 
        
        var foodc = await _context.FoodCategories.FindAsync(id);
        
        if(foodc == null) {
            return NoContent();
        }

        var foodca = await _context.FoodCategories.Where(fc => fc.Id == id).Select(fc => new { 
            fc.Code,
            fc.Description}).ToListAsync();

            return Ok(new {
                FoodCategoriesId = id,
                FoodCa = foodc
            });
    }

    [HttpGet("FoodCategories")]
    public async Task<IActionResult> GetAllFoodCategories(int pagenumber, int pagesize, string? foodstartwith = null) {
        if (pagenumber <= 0 || pagesize <= 0){return BadRequest("Bad Request Paramteres should be greater than zero");}

         var foodQ =  _context.FoodCategories.AsQueryable();

            if(!string.IsNullOrEmpty(foodstartwith)) { 

                foodQ = foodQ.Where(fq => fq.Description != null && fq.Description.StartsWith(foodstartwith));
            }

            var totalFoods = await foodQ.CountAsync();
            var totalPage = totalFoods == 0 ? 1 : (int)Math.Ceiling(totalFoods / (double)pagesize);

            var foodcategories = await foodQ 
            .OrderBy(fq => fq.Description)
            .Skip((pagenumber - 1) * pagesize)
            .Take(pagesize)
            .Select(fq => new {

                fq.Code,
                fq.Description,
                fq.Id
            
                } ).ToListAsync();

            var response = new {
                TotalCount = totalFoods,
                TotalPages = totalPage,
                CurrentPage = pagenumber,
                PageSize = pagesize,
                Data = foodcategories
            };

            return Ok(response);
    }

 

    // POST: foodcategory/foodcategories
    [HttpPost("foodcategories/new")]
    public async Task<IActionResult> CreateFoodCategory([FromForm] int id, [FromForm] string code, [FromForm] string description)
    {
        if (id == null || code == null || description == null)
        {
            return BadRequest("FoodCategory data is required.");
        }

        // Check if a category with the same Code or Description already exists
        var existingCategory = await _context.FoodCategories
            .FirstOrDefaultAsync(fc => fc.Code == code || fc.Description == description);

        if (existingCategory != null)
        {
            return Conflict("A FoodCategory with the same Code or Description already exists.");
        }

        FoodCategory foodCa;
        foodCa = new FoodCategory {Id = id, Code = code, Description = description};

        await _context.FoodCategories.AddAsync(foodCa);
        await _context.SaveChangesAsync();

        return Ok(new {message = "New Food categories beed added", Id = id, Code = code, Description = description});
    }

    // PUT: foodcategory/foodcategories/{id}
    [HttpPut("foodcategories/update/{id:int}")]
    public async Task<IActionResult> UpdateFoodCategoryById(int id, [FromForm] string code, string description)
    {
        var foodc = await _context.FoodCategories.FindAsync(id);

        
        if (foodc == null)
        {
            return NotFound("FoodCategory not found.");
        }

        foodc.Code = code;
        foodc.Description = description;

        _context.FoodCategories.Update(foodc);
        await _context.SaveChangesAsync();

        return Ok(new {message = "FoodCategories successfully update"});
    }

    

    // DELETE: foodcategory/foodcategories/delete/{id}
    [HttpDelete("foodcategories/delete/{id:int}")]
    public async Task<IActionResult> DeleteFoodCategoryById(int id)
    {
        var existingCategory = await _context.FoodCategories.FindAsync(id);
        if (existingCategory == null)
        {
            return NotFound("FoodCategory not found.");
        }

        _context.FoodCategories.Remove(existingCategory);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "FoodCategory deleted successfully." });
    }

    
}


