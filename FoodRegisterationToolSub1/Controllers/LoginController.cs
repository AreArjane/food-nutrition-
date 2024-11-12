using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Controller for håndtering av brukerinnlogging.
/// </summary>
[Route("Login")]
public class LoginController : Controller
    {
        // Action to serve the login view
        /// <summary>
    /// Viser innloggingssiden for brukeren.
    /// </summary>
    /// <returns>
    /// En <see cref="IActionResult"/> som representerer visningen av innloggingssiden.
    /// </returns>
        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }
    }
