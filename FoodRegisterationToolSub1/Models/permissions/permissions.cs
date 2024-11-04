namespace FoodRegisterationToolSub1.Models.permissions;

public class Permission { 
    public int PermissionId {get; set;}
    public PermissionType PermissionType {get; set;}


}
namespace FoodRegisterationToolSub1.Models.permissions;
public enum PermissionType { 
//NormalUser Permissions
    ViewOwnData, 
    EditOwnData, 
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

}