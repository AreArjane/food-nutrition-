using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using EmailService;
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

    private bool IsCodeInUse(string code, string email) {
        return _context.PendingSuperUser.Any(pu => pu.Email == email && pu.verificationcode == code);
    }
//********************************************************************Render Sub1 Frontend********************************************************************************//
    [HttpGet("")]
    
    public IActionResult Logup() {
        return View("/Views/Login/logup.cshtml");
    }


//******************************************************************Log Up function******************************************************************************************//
    [HttpPost("s")]
    public async Task<IActionResult> LogupSubmitNormalUser(
        [FromForm] string firstname, [FromForm] string? lastname, 
        [FromForm] string phonenr, [FromForm] string email, 
        [FromForm] string password,  
        [FromForm] string? dateofbirth, [FromForm] UserType usertype) {

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
                    newUser = new PendingSuperUser {
                    FirstName = firstname,
                    PhoneNr = phonenr,
                    Email = email,
                    DateOfBirth = dateofbirth,
                    verificationcode = verificationCode
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
                
                    if(normalUserExist == null ) { _context.NormalUsers.Add((NormalUser)newUser);}
                
                    else { Console.WriteLine("Redirecting to /Login"); return Redirect("http://localhost:5072/Login");}                 break;
                
                case UserType.SuperUser:
                
                            
                    var PuserExist = _context.PendingSuperUser.FirstOrDefault(nu => nu.Email == email);
                 
                    if(PuserExist == null) { _context.PendingSuperUser.Add((PendingSuperUser)newUser);} 
                 
                    else if(PuserExist != null && PuserExist.ExpirationCheck() == true) { }
                    else { Console.WriteLine("Redirecting to /Login"); return Redirect("http://localhost:5072/Login"); }  break;
                
                default:                                throw new ArgumentException("Invalid user type.");
            }
            //

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
