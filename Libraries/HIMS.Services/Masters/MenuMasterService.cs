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
            return await DatabaseHelper.GetGridDataBySp<MenuMasterListDto>(model, "m_Rtrv_Menu_master");
        }
       
        public virtual async Task InsertAsyncSP(MenuMaster objMenuMaster, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Id", "RoleId", "IsView", "IsAdd", "IsEdit", "IsDelete" };
            var entity = objMenuMaster.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Insert_MenuMaster_1", CommandType.StoredProcedure, entity);

        }
        public virtual async Task UpdateAsyncSP(MenuMaster objMenuMaster, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RoleId", "IsView", "IsAdd", "IsEdit", "IsDelete" };
            var entity = objMenuMaster.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_MenuMaster_1", CommandType.StoredProcedure, entity);

        }
    }
}
