using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using LinqToDB;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Data.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Administration;

namespace HIMS.Services.Masters
{
    public class ParameterMasterService : IParameterMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ParameterMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<MPathParameterListDto>> MPathParameterList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MPathParameterListDto>(model, "m_Rtrv_PathParameterMaster_by_Name");
        }

        public virtual async Task InsertAsync(MPathParameterMaster objPara, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // remove conditional records
                if (objPara.IsNumeric == 0)
                    objPara.MParameterDescriptiveMasters = null; 
                else
                    objPara.MPathParaRangeMasters = null;
                // Add header table records

                _context.MPathParameterMasters.Add(objPara);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MPathParameterMaster objPara, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MPathParameterMasters.Update(objPara);
                _context.Entry(objPara).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task CancelAsync(MPathParameterMaster objPara, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                MPathParameterMaster objpathology = await _context.MPathParameterMasters.FindAsync(objPara.ParameterId);
                objpathology.IsActive = false;
                objpathology.CreatedDate = objPara.CreatedDate;
                objpathology.CreatedBy = objPara.CreatedBy;
                _context.MPathParameterMasters.Update(objpathology);
                _context.Entry(objpathology).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
