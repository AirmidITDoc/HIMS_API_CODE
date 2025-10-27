using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.Permissions
{
    public class PermissionService : IPermissionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PermissionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<List<PageMasterDto>> GetAllModules(long RoleId)
        {
            var query = from M in _context.MenuMasters
                        join P in _context.PermissionMasters on
                        new
                        {
                            Key1 = M.Id,
                            Key2 = true
                        }
                        equals
                        new
                        {
                            Key1 = P.MenuId,
                            Key2 = P.RoleId == RoleId
                        } into tmpPermission
                        from P in tmpPermission.DefaultIfEmpty()
                        where M.IsActive && !string.IsNullOrWhiteSpace(M.PermissionCode)
                        orderby M.Id
                        select new PageMasterDto()
                        {
                            PageName = M.LinkName,
                            PageCode = M.PermissionCode,
                            IsAdd = P.IsAdd,
                            IsDelete = P.IsDelete,
                            IsEdit = P.IsEdit,
                            IsView = P.IsView,
                            IsExport = P.IsExport
                        };

            return await query.ToListAsync();
        }
    }
}
