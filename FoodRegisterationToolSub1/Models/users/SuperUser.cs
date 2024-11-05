
using System.ComponentModel.DataAnnotations;
using FoodRegisterationToolSub1.Models.permissions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRegisterationToolSub1.Models.users.User;
/// <summary>
/// SuperUser models extend the User class. SuperUser are known like employess of the system.
/// This models registred for an aditional fields assoicated with the employess regulations. 
/// </summary>
[Table("SuperUser")]
public class SuperUser : User { 

    
    [StringLength(6)]
    public required string DateOfBirth {get; set;}
 

    public override List<Permission> Permissions => new List<Permission> {
        new Permission { PermissionType = PermissionType.EditOwnData},
        new Permission { PermissionType = PermissionType.ViewOwnData},
        new Permission { PermissionType = PermissionType.ModifyFirstNameUser},
        new Permission { PermissionType = PermissionType.ModifyLastNameUser},


    };

}