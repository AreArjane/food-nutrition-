using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoodRegisterationToolSub1.Models.permissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRegisterationToolSub1.Models.users
{
    /// <summary>
    /// SuperUser models extend the User class. SuperUsers are employees of the system
    /// with additional fields associated with employee regulations.
    /// </summary>
    [Table("SuperUser")]
    public class SuperUser : User
    {
        
        public string? DateOfBirth { get; set; }

        public string? Email { get; set; }

        public SuperUser() : base(UserType.SuperUser) {}

        public override List<Permission> Permissions => new List<Permission>
        {
            new Permission { PermissionType = PermissionType.EditOwnData },
            new Permission { PermissionType = PermissionType.ViewOwnData },
            new Permission { PermissionType = PermissionType.ModifyFirstNameUser },
            new Permission { PermissionType = PermissionType.ModifyLastNameUser }
        };
    }
}
