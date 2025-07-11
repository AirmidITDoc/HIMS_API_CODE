using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public  class OTService : IOTService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OTService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OTBookinglistDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OTBookinglistDto>(model, "m_Rtrv_OTBookinglist");
        }
       
        public virtual async Task InsertAsync(TOtbooking OBJTOtbooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TOtbookings.Add(OBJTOtbooking);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TOtbooking OBJTOtbooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TOtbookings.Update(OBJTOtbooking);
                _context.Entry(OBJTOtbooking).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
