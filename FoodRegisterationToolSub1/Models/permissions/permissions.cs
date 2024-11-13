namespace FoodRegisterationToolSub1.Models.permissions;
/// <summary>
/// The permissions class of the User. Define a set of the permissions. 
/// The Permissions are allocated enumarted in the set of permissions. 
/// Each type descripe the action of the permissions. 
/// </summary>
public class Permission { 
    public int PermissionId {get; set;}
    public PermissionType PermissionType {get; set;}


}

public enum PermissionType { 
//NormalUser Permissions
    ViewOwnData, 
    EditOwnData, 
    AddMeals,
    ModifyMeals,
//Admin Permissions
    ViewAll,
    EditAll,
    ManageUsers,
    RemoveSelectedUser, 
    RemoveAllUser,
    AddUser,
    ModifyUser,
//SuperUSer permission
ModifyFirstNameUser,
ModifyLastNameUser, 
ModifySpecificMeals,
AddFood,
ModifyFood,
UpdateFood,
AddNutrient,
ModifyNutrient,
UpdateNutrient,

None

}