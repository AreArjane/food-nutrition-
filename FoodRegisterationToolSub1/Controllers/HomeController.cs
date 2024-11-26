using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Controller for håndtering av hjemmesiden til applikasjonen.
/// </summary>
public class HomeController : Controller
{
/// <summary>
    /// Henter startsiden for applikasjonen.
    /// </summary>
    /// <returns>
    /// En <see cref="IActionResult"/> som representerer visningen av startsiden.
    /// </returns>
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
 
      
        
}
