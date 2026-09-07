using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public  class ConstantService : IConstantService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ConstantService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ConstantsListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ConstantsListDto>(model, "ps_Rtrv_M_Constants_List");
        }
        public List<SearchConstantsDto> SearchConstants(string Keyword)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Keyword", Keyword);
            return sql.FetchListBySP<SearchConstantsDto>("ps_rtrv__ConstantType_Dropdown", para);
        }
        public virtual async Task Update(MConstant ObjMConstant, int CurrentUserId, string CurrentUserName)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            string[] AEntity = { "ConstantId", "Name", "Value" };
            var pentity = ObjMConstant.ToDictionary();
            foreach (var rProperty in pentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    pentity.Remove(rProperty);
            }

            odal.ExecuteNonQueryNew("ps_Update_M_Constants", CommandType.StoredProcedure,"", pentity);
            await _context.LogProcedureExecution(pentity, nameof(MConstant), ObjMConstant.ConstantId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            // Save audit log changes
            await _context.SaveChangesAsync();

            // Commit transaction
            await transaction.CommitAsync();
            }
            catch
            {
                // Rollback transaction on error
                await transaction.RollbackAsync();
                throw;
            }

        }

    }
}
