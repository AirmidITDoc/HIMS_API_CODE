using HIMS.Data.DTO.User;
using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IRoleService
    {
        List<MenuModel> GetPermisison(int RoleId);
        void SavePermission(List<PermissionModel> lst);
    }
}
