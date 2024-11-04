using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using FoodRegisterationToolSub1.Models.permissions;

namespace FoodRegisterationToolSub1.Models.users.User;
public abstract class User { 
    public int UserId {get; set;}
    [Required]
    [StringLength(60)]
    public string FirstName {get; set;}
    [RegularExpression(@"^\d{14}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string phoneNr {get; set;}
    public abstract List<Permission> Permissions {get;}

    public bool HasPermission(PermissionType permissionType) { 
        return Permissions.Any(p => p.permissionType == permissionType);
    }
}