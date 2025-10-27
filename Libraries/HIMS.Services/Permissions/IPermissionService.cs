using HIMS.Data.Models;

namespace HIMS.Services.Permissions
{
    public partial interface IPermissionService
    {
        Task<List<PageMasterDto>> GetAllModules(long RoleId);
    }
}
