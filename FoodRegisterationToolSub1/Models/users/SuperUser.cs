
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using FoodRegisterationToolSub1.Models.permissions;

namespace FoodRegisterationToolSub1.Models.users.User;

public class SuperUser : User { 

    [StringLength(25)]
    public string firstName {get; set;}
    [StringLength(6)]
    public string dateofbirth {get; set;}
    [StringLength(14)]
    public string phoneNr {get; set;}

    public override List<Permission> Permissions => new List<Permission> {
        new Permission { PermissionType = PermissionType.EditOwnData},
        new Permission { PermissionType = PermissionType.ViewOwnData},
        new Permission { PermissionType = PermissionType.ModifyFirstNameUser},
        new Permission { PermissionType = PermissionType.ModifyLastNameUser},


    };

}