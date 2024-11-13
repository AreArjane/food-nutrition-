using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoodRegisterationToolSub1.Models.permissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRegisterationToolSub1.Models.users
{
    /// <summary>
    /// PendingSuperUser models extend the User class. SuperUsers are employees of the system
    /// with additional fields associated with employee regulations.
    /// </summary>
    [Table("PendingSuperUser")]
    public class PendingSuperUser : User
    {
        public string DateOfBirth { get; set; }

       
        public string? Email { get; set; }

        public bool isApproved {get; set;} = false;

        public PendingSuperUser() : base(UserType.SuperUser)
        {
            
        }

        public override List<Permission> Permissions => new List<Permission> {
            new Permission  { PermissionType = PermissionType.None }

        };
    }
}
