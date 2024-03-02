using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Permissions
{
    public partial interface IPermissionService
    {
        Task<List<PageMaster>> GetAllModules(long RoleId);
    }
}
