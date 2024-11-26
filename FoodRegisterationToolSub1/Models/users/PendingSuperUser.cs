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

        public bool isApproved {get; private set;} = false;
        public string verificationcode {get; set;}

        public DateTime datacreated {get; private set;}

        public PendingSuperUser() : base(UserType.SuperUser)
        {
            datacreated = DateTime.UtcNow;
            
        }

        public void ApprovPendingUser() {
            isApproved = true;
        }

        public bool ExpirationCheck() {

            DateTime expirationTime = datacreated.AddHours(48);
            if(DateTime.UtcNow > expirationTime) {
            
                return true;
            }

            return false;
        }

        public override List<Permission> Permissions => new List<Permission> {
            new Permission  { PermissionType = PermissionType.None }

        };
    }
}
