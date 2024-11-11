using Microsoft.AspNetCore.Mvc;
[Route("Login")]
public class LoginController : Controller
    {
        // Action to serve the login view
        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }
    }
