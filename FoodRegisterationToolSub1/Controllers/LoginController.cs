using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using EmailService;
using FoodRegisterationToolSub1.Models.users;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
/// <summary>
/// Controller responsible for handling user login and registration (log up) actions.
/// </summary>
[Route("Login")]
[ApiController]
public class LoginController : Controller
    {
 /// <summary>
    /// Renders the login page view.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> rendering the login page.</returns>

        [HttpGet("")]
        public IActionResult Login()
        {
            return View();
        }
    }
/// <summary>
/// Controller responsible for user registration (log up) functionality, including form validation and database operations.
/// </summary>
[Route("set")]
[ApiController]
public class LogUpController : Controller {
/// <summary>
    /// Instance of the application's database context for managing user-related data.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogUpController"/> class with the provided database context.
    /// </summary>
    /// <param name="context">Database context for user registration.</param>
    

    public LogUpController(ApplicationDbContext context) { 
        _context = context;
       

    }
   /// <summary>
    /// Checks if a given verification code is already associated with an email in the database.
    /// </summary>
    /// <param name="code">Verification code to check.</param>
    /// <param name="email">Email address to check the code against.</param>
    /// <returns>True if the code is in use; otherwise, false.</returns>
    private bool IsCodeInUse(string code, string email) {
        return _context.PendingSuperUser.Any(pu => pu.Email == email && pu.verificationcode == code);
    }
//********************************************************************Render Sub1 Frontend********************************************************************************//
   /// <summary>
    /// Renders the log up page view for user registration.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> rendering the log up page view.</returns>
    [HttpGet("")]
    
    public IActionResult Logup() {
        return View("/Views/Login/logup.cshtml");
    }


//******************************************************************Log Up function******************************************************************************************//
   /// <summary>
    /// Handles user registration submissions, validating input data and storing new users in the database.
    /// </summary>
    /// <param name="firstname">The first name of the user.</param>
    /// <param name="lastname">The last name of the user (optional).</param>
    /// <param name="phonenr">The phone number of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="password">The password for the new user account.</param>
    /// <param name="dateofbirth">The date of birth of the user (optional).</param>
    /// <param name="usertype">The type of the user (NormalUser or SuperUser).</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the registration.</returns>
    [HttpPost("s")]
    public async Task<IActionResult> LogupSubmitNormalUser(
        [FromForm] string firstname, [FromForm] string? lastname, 
        [FromForm] string phonenr, [FromForm] string email, 
        [FromForm] string password,  
        [FromForm] string? dateofbirth, [FromForm] UserType usertype) {
     // Log request details
        Console.WriteLine($"Received request with Email: {email}");
        Console.WriteLine($"Firstname: {firstname}, Lastname: {lastname}, Phonenr: {phonenr}, Dateof birth {dateofbirth}, usertype: {usertype }");


//******************************************************Validation Input Block**********************************************************************//
       var validatorError = new List<String>(); 
       var firstnameValidate = ValidatorSpes.ValidateNorwegianAlphabet(firstname)[0];
       var lastnameValidate = ValidatorSpes.ValidateNorwegianAlphabet(lastname)[0];
       var emailEntity = new EmailAddressAttribute();
       var EmailValidate = ValidatorSpes.IsEmail(email)[0];
       var phonenrValidate = ValidatorSpes.IsOnlyNumber(phonenr)[0];
       var usertypeValidate = ValidatorSpes.IsValidUserType(usertype);
       var dataofbirthNormaluser = string.IsNullOrEmpty(dateofbirth);
       var lastnameSuperuser = string.IsNullOrEmpty(lastname);
       var dateofbirthValidate = ValidatorSpes.IsOnlyNumber(dateofbirth)[0];

//***************************************************Validation decistion based on User Type*******************************************************//
      
       switch (usertype) { 

        case UserType.NormalUser: 

        if(!firstnameValidate)    {      validatorError.Add("Error with firstname input : Only Norwegian alfabet");                          }
        if(!lastnameValidate)     {      validatorError.Add("Error with lastname input  : Only Norwegian accepted input");                   }
        if(!EmailValidate)        {      validatorError.Add("Error with Email input     : Email format example@example.com");                }
        if(!phonenrValidate)      {      validatorError.Add("Error with phonenr input   : Phone Number format only number 8-14");            }
        if(!dataofbirthNormaluser){      validatorError.Add("Error with Date of Birth   : Date of Birth is not expected with Normal User");  } 

        break;

        case UserType.SuperUser:

        if(!firstnameValidate)    {      validatorError.Add("Error with firstname input : Only Norwegian alfabet");                          }
        if(!EmailValidate)        {      validatorError.Add("Error with Email input     : Email format example@example.com");                }
        if(!phonenrValidate)      {      validatorError.Add("Error with phonenr input   : Phone Number format only number 8-14");            }
        if(!lastnameSuperuser)    {      validatorError.Add("Error with Last Name       : Last name is not expected with Super User");       } 
        if(!dateofbirthValidate)  {      validatorError.Add("Error with date of birth   : Only number format accepted e.g 01012024");        }

        break;

        default: 

        validatorError.Add("Error with Usertype : Not defined");
        break;
} 


    if(validatorError.Count > 0) { return BadRequest(new {errors = validatorError}); }


//***************************************************Verification with Email OTP*************************************************************************************//

string verificationCode; 

do { 
    verificationCode = new Random().Next(100000, 999999).ToString();

} while(IsCodeInUse(verificationCode, email));







//***************************************************Populate the input for creating*************************************************************************************//

    try
        {
            User newUser;

            switch(usertype) {
                
                case UserType.NormalUser: 
                
                
                    newUser = new NormalUser {
                        FirstName = firstname,
                        LastName = lastname,
                        PhoneNr = phonenr,
                        Email = email,
                    
                    }; 
                    break;

                case UserType.SuperUser:
                    newUser = new SuperUser {
                    FirstName = firstname,
                    PhoneNr = phonenr,
                    Email = email,
                    DateOfBirth = dateofbirth
                    }; 
                    break;

                default:  throw new ArgumentException("Invalid user type.");
    }
    
//******************************************************************Password Creating***************************************************************************//
    newUser.SetPassword(password);

Console.WriteLine(newUser.UserType);
//*****************************************************************Add User to Database***************************************************************************//
    switch (newUser.UserType)
            {
                case UserType.NormalUser:  
                    

                    var normalUserExist = _context.NormalUsers.FirstOrDefault(n => n.Email == email);
                
                    if(normalUserExist == null ) { _context.NormalUsers.Add((NormalUser)newUser); await EmailVerification.SendVerificationCode(email, verificationCode);}
                
                    else { Console.WriteLine("Redirecting to /Login"); return Redirect("http://localhost:5072/Login");}                 break;
                
                case UserType.SuperUser:
                
                            
                    var PuserExist = _context.SuperUsers.FirstOrDefault(nu => nu.Email == email);
                 
                    if(PuserExist == null) { _context.SuperUsers.Add((SuperUser)newUser); await EmailVerification.SendVerificationCode(email, verificationCode);} 
                 
                    else if(PuserExist != null) { await EmailVerification.SendVerificationCode(verificationCode, email);}
                    else { Console.WriteLine("Redirecting to /Login"); return Redirect("http://localhost:5072/Login"); }  break;
                
                default:                                throw new ArgumentException("Invalid user type.");
            }

    using var databaseTransaction =await _context.Database.BeginTransactionAsync();

    try {

            await _context.SaveChangesAsync();
            await databaseTransaction.CommitAsync();

    } catch {
                await databaseTransaction.RollbackAsync();
                throw;
            }

    return Ok( new {message = "User registration successful"});
    }


//*************************************************************Error Creating User with Database *****************************************************************//
        catch (ValidationException ex) { return BadRequest(new { error = ex.Message }); }
        catch (ArgumentException ex)   { return BadRequest(new { error = ex.Message }); }
    }
}
