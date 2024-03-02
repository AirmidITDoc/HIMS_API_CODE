using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly Data.Models.SSDB_JSS_WEB_25SepContext _context;
        public PermissionService(SSDB_JSS_WEB_25SepContext sSDB_JSS_WEB_25SepContext)
        {
            _context = sSDB_JSS_WEB_25SepContext;
        }
        public virtual async Task<List<PageMaster>> GetAllModules(long RoleId)
        {
            var query = from PM in _context.PageMasters
                        join M in _context.ModuleMasters on PM.ModuleId equals M.Id
                        join P in _context.PermissionMasters on
                        new
                        {
                            Key1 = PM.Id,
                            Key2 = true
                        }
                        equals
                        new
                        {
                            Key1 = P.PageId,
                            Key2 = P.RoleId == RoleId
                        } into tmpPermission
                        from P in tmpPermission.DefaultIfEmpty()
                        where M.IsActive && PM.IsActive
                        orderby M.Id
                        select new PageMaster()
                        {
                            ModuleName = M.ModuleName,
                            Id = PM.Id,
                            PageName = PM.PageName,
                            PageCode = PM.PageCode,
                            ModuleId = PM.ModuleId,
                            RoleId = RoleId,
                            IsAdd = P.IsAdd,
                            IsDelete = P.IsDelete,
                            IsEdit = P.IsEdit,
                            IsView = P.IsView,
                            PermissionId = P.Id
                        };

            return await query.ToListAsync();
        }
    }
}
