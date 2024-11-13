using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FoodRegisterationToolSub1.Models.permissions;
using FoodRegisterationToolSub1.Models.meals;
using Microsoft.AspNetCore.Identity;

namespace FoodRegisterationToolSub1.Models.users
{
    public enum UserType
    {
        NormalUser,
        SuperUser,
        AdminUser,
        PendingSuperUser
    }

    /// <summary>
    /// User Abstraction Class. There are three types of users in the system:
    /// NormalUser, SuperUser, and AdminUser. 
    /// All Users are required to provide FirstName and PhoneNumber. A unique UserId is generated for each User. 
    /// </summary>
    /// <remarks>
    /// Each user is configured with the permissions models. This model gives specific permissions to each
    /// user on the system's functions. Admin and SuperUser utilize this model for separation of duties.
    /// </remarks>
    public abstract class User
    {
        [Key]
        public int UserId { get; private set; }

        [Required]
        [StringLength(60)]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ]{1,155}$", ErrorMessage = "Invalid characters in First name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(17)]
        [RegularExpression(@"^\d{1,17}$", ErrorMessage = "Phone number must contain only digits and be up to 17 digits long.")]
        public string? PhoneNr { get; set; }

        [Required]
        public string Password { get; private set; }

        public UserType UserType { get; private set; }

        protected User(UserType userType)
        {
            UserId = GenerateUserID();
            UserType = userType;
        }

        private int GenerateUserID()
        {
            return BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        }

        public abstract List<Permission> Permissions { get; }

        public bool HasPermission(PermissionType permissionType)
        {
            return Permissions.Any(p => p.PermissionType == permissionType);
        }

        public void SetPassword(string password)
        {
            var passwordHash = new PasswordHasher<User>();
            Password = passwordHash.HashPassword(this, password);
        }

        public bool VerifyPassword(string plainPassword)
        {
            var passwordHash = new PasswordHasher<User>();
            var result = passwordHash.VerifyHashedPassword(this, Password, plainPassword);
            return result == PasswordVerificationResult.Success;
        }

        

        public ICollection<Meal> Meals { get; set; }
    }
}
