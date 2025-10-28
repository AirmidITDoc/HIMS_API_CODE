using HIMS.Data.Models;

namespace HIMS.Services.Users
{
    public partial interface IMenuService
    {
        List<MenuModel> GetMenus(int RoleId, bool isActiveMenuOnly);
    }
}
