using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using FoodRegisterationToolSub1.Models.permissions;
using FoodRegisterationToolSub1.Models.meals;
using Microsoft.AspNetCore.Identity;

namespace FoodRegisterationToolSub1.Models.users {

    public enum UserType {
        NormalUser,
        SuperUser,
        AdminUser
    }

/// <summary>
/// User Abstraction Class. There is three type of user on the system.
/// NormalUser, SuperUser and AdminUser. 
/// All User required to provide firstName and PhoneNumber. An unique UserId generated for each User. 
/// </summary>
/// <remarks>
/// Each user configurated with the permissions models. This models developed to give specific permision for each
/// user on the system function. Admin and SuperUser with help of this models gets the sepration of duty. 
/// </remarks>
public abstract class User { 
    [Key]
    public int UserId {get; private set;}

    [Required]
    [StringLength(60)]
    public string? FirstName {get; set;}
    [Required]
    [RegularExpression(@"^\d{14}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string? PhoneNr {get; set;}

    [Required]
    public string Password {get; private set;}

    public UserType UserType {get; protected set;}

    public User() { 
        
        UserId = GenerateUserID();
    }

    private int GenerateUserID() { 
        return BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
    }

    public abstract List<Permission> Permissions {get;}

    public bool HasPermission(PermissionType permissionType) { 
        return Permissions.Any(p => p.PermissionType == permissionType);
    }

    public void setPassword(string password) { 
        var passwordHash = new PasswordHasher<User>();
        Password = passwordHash.HashPassword(this, password);
    }

    public bool VerifyPassword(string Plainpassword) { 
        var passwordHash = new PasswordHasher<User>();
        var result = passwordHash.VerifyHashedPassword(this, Password,Plainpassword);
        return result == PasswordVerificationResult.Success;
    }

    public ICollection<Meal> Meals {get; set;}
}
}