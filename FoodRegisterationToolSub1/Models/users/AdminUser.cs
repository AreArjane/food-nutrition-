
using System.ComponentModel.DataAnnotations;
using FoodRegisterationToolSub1.Models.permissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRegisterationToolSub1.Models.users {
/// <summary>
/// AdminUser the owner of the system. Has a full functionality to the system. 
/// Can access all models to manage, modify and delete.
/// </summary>
[Table("AdminUser")]
public class AdminUser : User {

    [StringLength(11)]
    public required string NationalIdenityNumber {get; set;}
    [StringLength(25)]
    public string? OfficeAddress { get;  set; }
    public string? WorkPhoneNr {get; set;}
    public AdminUser() : base(UserType.AdminUser){

        
    }


    public override List<Permission> Permissions => new List<Permission> {
        new Permission { PermissionType = PermissionType.EditAll },
         new Permission { PermissionType = PermissionType.RemoveAllUser },
          new Permission { PermissionType = PermissionType.ModifyUser },
           new Permission { PermissionType = PermissionType.ManageUsers },
            new Permission { PermissionType = PermissionType.AddUser },
             new Permission { PermissionType = PermissionType.ViewAll},
              new Permission { PermissionType = PermissionType.RemoveSelectedUser},

    };
}
}