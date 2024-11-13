using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

[Route("Login")]
[ApiController]
public class LoginController : Controller
    {


        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }
    }

[Route("set")]
[ApiController]
public class LogUpController : Controller {

    private readonly ApplicationDbContext _context;
    

    public LogUpController(ApplicationDbContext context) { 
        _context = context;
       

    }

    [HttpGet("")]
    
    public IActionResult Logup() {
        return View("/Views/Login/logup.cshtml");
    }

    [HttpPost("s")]
    public async Task<IActionResult> LogupSubmitNormalUser(
        [FromForm] string firstname, [FromForm] string? lastname, 
        [FromForm] string phonenr, [FromForm] string email, 
        [FromForm] string password,  
        [FromForm] string? dateofbirth, [FromForm] UserType usertype) {

        Console.WriteLine($"Received request with Email: {email}");
    Console.WriteLine($"Firstname: {firstname}, Lastname: {lastname}, Phonenr: {phonenr}, Dateof birth {dateofbirth}, usertype: {usertype }");

       var validatorError = new List<String>(); 
       var nameValidate = ValidatorSpes.ValidateNorwegianAlphabet(firstname, lastname);
       var emailEntity = new EmailAddressAttribute();
       var EmailValidate = ValidatorSpes.IsEmail(email);
       

       if(!nameValidate.All(result => result)) { 
        validatorError.Add("First name or last name cotnais invalid charachters"); }
       
       //if(!emailEntity.IsValid(email)) {
       // validatorError.Add("Email conatins invalid charachters");}
      
       if(!ValidatorSpes.IsPhoneNumber(phonenr)[0]) {
        validatorError.Add("Phone number invalid it should be only number");}
       
       

       //if(validatorError.Count > 0) {
        //return BadRequest(new {errors = validatorError});
       //}

      try
        {
            // Create the user based on UserType and save to database
            User user = usertype switch
    {
        UserType.NormalUser => new NormalUser
        {
            FirstName = firstname,
            LastName = lastname,
            PhoneNr = phonenr,
            Email = email
        },
        UserType.SuperUser =>  new SuperUser
            {
                FirstName = firstname,
                PhoneNr = phonenr,
                Email = email,
                DateOfBirth = dateofbirth
            } ,
            
        _ => throw new ArgumentException("Invalid user type.")
    };


            // Set the password for the user
            user.SetPassword(password);

            // Add the user to the specific table based on type
            switch (user)
            {
                case NormalUser normalUser:
                    _context.NormalUsers.Add(normalUser);
                    break;
                case SuperUser superUser:
                    _context.SuperUsers.Add(superUser);
                    break;
                default:
                    throw new ArgumentException("Invalid user type.");
            }

            await _context.SaveChangesAsync();

            return Ok( new {message = "User registration successful"});
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}