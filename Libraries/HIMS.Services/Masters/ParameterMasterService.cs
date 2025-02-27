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
                if (objPara.IsNumeric == 1)
                    objPara.MParameterDescriptiveMasters = null; 
                else
                    objPara.MPathParaRangeWithAgeMasters = null;
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
                if (objPara.IsNumeric == 1)
                {
                    // Delete record from Parameter range table IsNumeric : 0
                    var lst1 = await _context.MPathParaRangeWithAgeMasters.Where(x => x.ParaId == objPara.ParameterId).ToListAsync();
                    if (lst1 != null && lst1.Count > 0)
                    {
                        _context.MPathParaRangeWithAgeMasters.RemoveRange(lst1);
                    }
                    await _context.SaveChangesAsync(); // Save deletions before proceeding
                }
                else
                {
                    //Delete details table realted records // Delete record from Parameter Descriptive table IsNumeric : 1
                    var lst = await _context.MParameterDescriptiveMasters.Where(x => x.ParameterId == objPara.ParameterId).ToListAsync();
                    if (lst != null && lst.Count > 0)
                    {
                        _context.MParameterDescriptiveMasters.RemoveRange(lst);
                    }
                    await _context.SaveChangesAsync(); // Save deletions before proceeding
                }

                if (objPara.IsNumeric == 1)
                {
                    objPara.MParameterDescriptiveMasters = null; // Prevent re-inserting deleted records
                }
                else
                {
                    objPara.MPathParaRangeWithAgeMasters = null;
                }
                // Add header table records
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

        public virtual async Task UpdateFormulaAsync(MPathParameterMaster objPara, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                MPathParameterMaster objpathology = await _context.MPathParameterMasters.FindAsync(objPara.ParameterId);
                objpathology.Formula = objPara.Formula;
                _context.MPathParameterMasters.Update(objpathology);
                _context.Entry(objpathology).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
