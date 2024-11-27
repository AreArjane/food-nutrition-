


using FoodRegistrationToolSub1.Models.datasets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

///<summary>
///Class <c>NutrientController</c> a repository controller fo the Nutrients Models With CRUD operation follow API architecture.
///<example>
///Router all method should have a router name followed to the mehtod link /nutrientapi/*
///GET      /Nutrients                          -> return all record in the models with fixed pagenumber and pagesize (pagination).
///GET      /nutrient/{id:int}                  -> return the specific record by its Id given.
///POST     /create                             -> create a new record with id, code and description
///PUT      /update/{id:int}                    -> update the specified record by id
///DELETE   /delete/{id:int}                    -> delete the specified record by id
///</example>
///</summary>


[Route("nutrientapi")]
[ApiController]
public class NutrientController : Controller {

    private readonly ApplicationDbContext _context;

    public NutrientController(ApplicationDbContext context) {
        _context = context;
    }

    //GET
    [HttpGet("")]
    public IActionResult NutrientViewTable() {
        return View("/Views/Home/nutrient.cshtml");
    }

    [HttpGet("nutrient/{id}")]
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

    [HttpGet("Nutrients")]
    public async Task<IActionResult> GetAllNutrient(int pagenumber, int pagesize, string? nutrientstartwith = null) {
        if (pagenumber <= 0 || pagesize <= 0){return BadRequest("Bad Request Paramteres should be greater than zero");}

            var nutrientQ = _context.Nutrients.AsNoTracking();

            if(!string.IsNullOrEmpty(nutrientstartwith)) { 

                nutrientQ = nutrientQ.Where(fq => fq.Id != null && fq.Name.StartsWith(nutrientstartwith));
            }

            var totalNutrients = await nutrientQ.CountAsync();
            var totalPage = totalNutrients == 0 ? 1 : (int)Math.Ceiling(totalNutrients / (double)pagesize);

            var nutrients = await nutrientQ 
            .OrderBy(nq => nq.Name)
            .Skip((pagenumber - 1) * pagesize)
            .Take(pagesize)
            .Select(nq => new {

                nq.Id,
                nq.Name,
                nq.UnitName,
                nq.NutrientNbr,
                nq.Rank

            }).ToListAsync();

            var response = new {
                TotalCount = totalNutrients,
                TotalPages = totalPage,
                CurrentPage = pagenumber,
                PageSize = pagesize,
                data = nutrients
            };

            return Ok(response);
    }

    /// <summary>
    /// Create new Nutrient with name and unitname as required fields. 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="unitName"></param>
    /// <param name="nutrientNbr"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> CreateFood(
      [FromForm] string name,
     [FromForm] string unitName, 
     [FromForm] string? nutrientNbr, 
     [FromForm] string? rank)
     {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Name are required.");
        }

        //speeding up the request from 256.ms to 148.ms when using FirstOrDefualtAync() instead of AnyAsynch(). Tested with Postman
        var NameExist = await _context.Nutrients.FirstOrDefaultAsync(n => n.Name.ToUpper() == name.ToUpper()); 
            
        if(NameExist?.Name != null) { 
            return Conflict(new {message = $"The items alleredy exist and registered with the database"});
        }
            

        int newId;
        var maxId = await _context.Nutrients.MaxAsync(n => (int?)n.Id) ?? 999;
        newId = maxId + 1;

           

        var newNutrient = new Nutrient
        {
            Id = newId,
            Name = name,
            UnitName = unitName,
            NutrientNbr = !string.IsNullOrEmpty(nutrientNbr) ? nutrientNbr : null,
            Rank = !string.IsNullOrEmpty(rank) ? rank : null
        };

        await _context.Nutrients.AddAsync(newNutrient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFoodDetails), new { id = newNutrient.Id }, newNutrient);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="dataType"></param>
    /// <param name="publicationDate"></param>
    /// <returns></returns>
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateFood(int id, 
    [FromForm] string? name, [FromForm] string? unitName, [FromForm] string? nutrientNbr, string? rank)
    {
        var existingNutrient = await _context.Nutrients.FindAsync(id);
        if (existingNutrient == null)
        {
            return NotFound($"Nutrient with the Given ID ${id} not found.");
        }

      
        if (!string.IsNullOrEmpty(name)) existingNutrient.Name = name;
        if (!string.IsNullOrEmpty(unitName)) existingNutrient.UnitName = unitName;
        if (!string.IsNullOrEmpty(nutrientNbr)) existingNutrient.NutrientNbr = nutrientNbr;
        if (!string.IsNullOrEmpty(rank)) existingNutrient.Rank = rank;

       
        await _context.SaveChangesAsync();

        return Ok(existingNutrient);
    }
    
    // DELETE: foodapi/delete/{id}
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteFood(int id)
    {   //The FoodNutrient has a model CASCADE delete with Nutrient tabel which is no need to check the FOODNutrient table also. if check time speed of 2.54 s required. 
        //we checked with the min value first of the id record first
        var min = await _context.Nutrients.MinAsync(n => n.Id); var max = await _context.Nutrients.MaxAsync(n => n.Id);

        if(id < min || id > max) {return BadRequest("The ID cannot be found it in the database records");} 

        var existingNutrients = await _context.Nutrients.FindAsync(id);
        
        if (existingNutrients == null){ return NotFound("Food not found.");}

        _context.Nutrients.Remove(existingNutrients);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Nutrients with {id} deleted successfully." });
    }
}
