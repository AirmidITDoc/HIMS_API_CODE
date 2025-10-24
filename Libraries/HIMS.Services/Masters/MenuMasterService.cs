using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Masters
{
    public class MenuMasterService : IMenuMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MenuMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<MenuMasterListDto>> MenuMasterList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MenuMasterListDto>(model, "ps_Rtrv_Menu_master");
        }
        public virtual void InsertSP(MenuMaster objMenuMaster, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RoleId", "IsView", "IsAdd", "IsEdit", "IsDelete", "PermissionMasters" };
            var entity = objMenuMaster.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vId = odal.ExecuteNonQuery("ps_Insert_MenuMaster_1", CommandType.StoredProcedure, "Id", entity);

        }
        public virtual void UpdateSP(MenuMaster objMenuMaster, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RoleId", "IsView", "IsAdd", "IsEdit", "IsDelete", "PermissionMasters" };
            var entity = objMenuMaster.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_MenuMaster_1", CommandType.StoredProcedure, entity);

        }
    }
}
