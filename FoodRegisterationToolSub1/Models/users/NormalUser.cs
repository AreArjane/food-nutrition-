using System.ComponentModel.DataAnnotations;
using FoodRegisterationToolSub1.Models.permissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRegisterationToolSub1.Models.users
{
    /// <summary>
    /// NormalUser extends the User model. The User has basic permissions for what they own.
    /// </summary>
    [Table("NormalUser")]
    public class NormalUser : User
    {
        
        public string? LastName { get; set; }
        public string? Email { get; set; }

        [StringLength(25)]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ0-9\s]{1,255}$", ErrorMessage = "Invalid characters in Address")]
        public string? HomeAddress { get; set; }

        [StringLength(8)]
        [RegularExpression(@"^[0-9]{1,8}$", ErrorMessage = "Postal Code should only contain numbers")]
        public string? PostalCode { get; set; }

        public NormalUser() : base(UserType.NormalUser) { }

        public override List<Permission> Permissions => new List<Permission>
        {
            new Permission { PermissionType = PermissionType.ViewOwnData },
            new Permission { PermissionType = PermissionType.EditOwnData }
        };
    }
}
