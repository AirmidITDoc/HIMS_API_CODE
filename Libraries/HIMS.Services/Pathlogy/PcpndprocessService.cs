using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Pathlogy
{
    public  class PcpndprocessService : IPcpndprocesService 
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PcpndprocessService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<RadioPcpndtListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RadioPcpndtListDto>(model, "ps_Rtrv_RadioPcpndtList");
        }



        public virtual async Task InsertAsync( TPcpndprocess ObjTPcpndprocess, int UserId,string Username)
        {
            using var scope = new TransactionScope( TransactionScopeOption.Required,new TransactionOptions {IsolationLevel =   System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                ObjTPcpndprocess.CreatedBy = UserId;
                ObjTPcpndprocess.CreatedDate = AppTime.Now;

                _context.TPcpndprocesses.Add(ObjTPcpndprocess);

                await _context.SaveChangesAsync();

                scope.Complete();
            }
            catch (Exception ex)
            {
                throw new Exception( ex.InnerException != null  ? ex.InnerException.Message  : ex.Message);
            }
        }
        public virtual async Task UpdateAsync(TPcpndprocess ObjTPcpndprocess, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long PcpndtprocessId = ObjTPcpndprocess.PcpndtprocessId;

                // Delete related details first
                var lstAttend = await _context.TPcpndprocessDetails
                    .Where(x => x.PcpndtprocessId == PcpndtprocessId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TPcpndprocessDetails.RemoveRange(lstAttend);

            
                //Save deletion first
                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjTPcpndprocess);
                _context.Entry(ObjTPcpndprocess).State = EntityState.Modified;

                _context.Entry(ObjTPcpndprocess).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTPcpndprocess).Property(x => x.CreatedDate).IsModified = false;

                ObjTPcpndprocess.ModifiedBy = UserId;
                ObjTPcpndprocess.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTPcpndprocess).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

    }
}
