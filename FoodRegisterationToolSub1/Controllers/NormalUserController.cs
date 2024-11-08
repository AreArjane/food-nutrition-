
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class NormalUserController : ControllerBase { 

    private readonly ApplicationDbContext _context;

    public NormalUserController(ApplicationDbContext context) {
        _context = context;
    }


    [HttpGet("{id}/profile")]
    public IActionResult GetNormalUserProfile(int id) { 

        var normal_use = _context.NormalUsers
        .Where(nu => nu.UserId == id)
        .Select(nu => new {
            nu.
        })
    }
}