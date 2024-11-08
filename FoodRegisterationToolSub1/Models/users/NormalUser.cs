
using System.ComponentModel.DataAnnotations;
using FoodRegisterationToolSub1.Models.permissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodRegisterationToolSub1.Models.users {
/// <summary>
/// NormalUser extend the User models. The User get a basic permissions of what they own. 
/// Byond the information they manage on the system: 
/// 1- User private information data 
/// 
/// </summary>
[Table("NormalUser")]
public class NormalUser : User {

    [StringLength(25)]
    public string? LastName {get; set;}
    [StringLength(50)]
    public string? Email {get; set;}
    [StringLength(25)]
    public string? HomeAddress {get; set;}
    [StringLength(8)]
    public string? PostalCode {get; set;}

    public override List<Permission> Permissions => new List<Permission> { 
        new Permission { PermissionType = PermissionType.ViewOwnData },
        new Permission { PermissionType = PermissionType.EditOwnData }
     
    };
}}