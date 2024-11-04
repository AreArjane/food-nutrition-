
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using FoodRegisterationToolSub1.Models.permissions;

namespace FoodRegisterationToolSub1.Models.users.User;
public class NormalUser : User {

    [StringLength(25)]
    public string lastname {get; set;}
    [StringLength(50)]
    public string Email {get; set;}
    [StringLength(25)]
    public string address {get; set;}
    [StringLength(4)]
    public string postalcode {get; set;}

    public override List<Permission> Permissions => new List<Permission> { 
        new Permission { PermissionType = PermissionType.ViewOwnData },
        new Permission { PermissionType = PermissionType.EditOwnData }
     
    };
}