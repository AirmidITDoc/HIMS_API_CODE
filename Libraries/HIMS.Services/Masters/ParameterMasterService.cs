using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

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
        //public virtual async Task UpdateAsync(MPathParameterMaster objPara, int UserId, string Username, string[]? ignoreColumns = null)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Attach ONLY parent
        //        _context.MPathParameterMasters.Attach(objPara);

        //        _context.Entry(objPara).State = EntityState.Modified;

        //        // Ignore columns
        //        if (ignoreColumns?.Length > 0)
        //        {
        //            foreach (var column in ignoreColumns)
        //            {
        //                _context.Entry(objPara).Property(column).IsModified = false;
        //            }
        //        }
        //        if (objPara.IsNumeric == 1)
        //        {
        //            // Delete record from Parameter range table IsNumeric : 0
        //            var lst1 = await _context.MPathParaRangeWithAgeMasters.Where(x => x.ParaId == objPara.ParameterId).ToListAsync();
        //            if (lst1 != null && lst1.Count > 0)
        //            {
        //                _context.MPathParaRangeWithAgeMasters.RemoveRange(lst1);
        //            }
        //            await _context.SaveChangesAsync(); // Save deletions before proceeding
        //        }
        //        else
        //        {
        //            //Delete details table realted records // Delete record from Parameter Descriptive table IsNumeric : 1
        //            var lst = await _context.MParameterDescriptiveMasters.Where(x => x.ParameterId == objPara.ParameterId).ToListAsync();
        //            if (lst != null && lst.Count > 0)
        //            {
        //                _context.MParameterDescriptiveMasters.RemoveRange(lst);
        //            }
        //            await _context.SaveChangesAsync(); // Save deletions before proceeding
        //        }

        //        if (objPara.IsNumeric == 1)
        //        {
        //            objPara.MParameterDescriptiveMasters = null; // Prevent re-inserting deleted records
        //        }
        //        else
        //        {
        //            objPara.MPathParaRangeWithAgeMasters = null;
        //        }
        //        // Add header table records
        //        // Update header & detail table records
        //        _context.MPathParameterMasters.Update(objPara);
        //        _context.Entry(objPara).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
        public virtual async Task UpdateAsync(MPathParameterMaster objPara, int UserId, string Username, string[]? ignoreColumns = null)

        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            // Attach ONLY parent
            _context.MPathParameterMasters.Attach(objPara);

            _context.Entry(objPara).State = EntityState.Modified;

            // Ignore columns
            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(objPara).Property(column).IsModified = false;
                }
            }

            if (objPara.IsNumeric == 1)
            {
                var ranges = await _context.MPathParaRangeWithAgeMasters.Where(x => x.ParaId == objPara.ParameterId).ToListAsync();

                if (ranges.Count > 0) _context.MPathParaRangeWithAgeMasters.RemoveRange(ranges);
            }

            else
            {
                var desc = await _context.MParameterDescriptiveMasters.Where(x => x.ParameterId == objPara.ParameterId).ToListAsync();

                if (desc.Count > 0) _context.MParameterDescriptiveMasters.RemoveRange(desc);
            }


            await _context.SaveChangesAsync();

            scope.Complete();
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
