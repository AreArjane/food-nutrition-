using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Controller responsible for handling general application requests, such as the home page.
/// </summary>
[Route("")]
[ApiController]
public class HomeController : Controller
{
 /// <summary>
    /// Serves the home page of the application.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> that renders the home page view.</returns>
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
 
      
        
}
