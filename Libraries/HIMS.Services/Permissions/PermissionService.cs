using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using Microsoft.Data.SqlClient;
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
        private readonly Data.Models.HIMSDbContext _context;
        public PermissionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<List<PageMasterDto>> GetAllModules(long RoleId)
        {
            //    var query = from M in _context.MenuMasters
            //                join P in _context.PermissionMasters on
            //                new
            //                {
            //                    Key1 = M.Id,
            //                    Key2 = true
            //                }
            //                equals
            //                new
            //                {
            //                    Key1 = P.MenuId,
            //                    Key2 = P.RoleId == RoleId
            //                } into tmpPermission
            //                from P in tmpPermission.DefaultIfEmpty()
            //                where M.IsActive
            //                orderby M.Id
            //                select new PageMasterDto()
            //                {
            //                    PageName = M.LinkName,
            //                    PageCode = M.PermissionCode??"",
            //                    RoleId = RoleId,
            //                    IsAdd = P.IsAdd,
            //                    IsDelete = P.IsDelete,
            //                    IsEdit = P.IsEdit,
            //                    IsView = P.IsView,
            //                    PermissionId = P.Id
            //                };

            //    return await query.ToListAsync();

            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleId", RoleId);
            return sql.FetchListByQuery<PageMasterDto>("SELECT M.LinkName PageName,M.PermissionCode PageCode,@RoleId RoleId,ISNULL(P.IsAdd,0) IsAdd,ISNULL(P.IsEdit,0) IsEdit,ISNULL(P.IsDelete,0) IsDelete,ISNULL(P.IsView,0) IsView,P.Id PermissionId FROM MenuMaster M\r\nLEFT JOIN PermissionMaster P ON M.Id=P.MenuId AND P.RoleId=@RoleId\r\nWHERE M.IsActive=1", para);

        }
    }
}
