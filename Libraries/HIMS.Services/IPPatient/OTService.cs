using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Data.DataProviders;
using System.Data;
using System.Transactions;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells.Drawing;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.IPPatient;
using LinqToDB;

namespace HIMS.Services.IPPatient
{
    public class OTService : IOTService
    {
        private readonly HIMSDbContext _context;
        public OTService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
      


        public virtual async Task InsertAsync(TOtbookingRequest objOTBooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TOtbookingRequests.Add(objOTBooking);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TOtbookingRequest objOTBooking, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TOtbookingRequests.Update(objOTBooking);
                _context.Entry(objOTBooking).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task CancelAsync(TOtbookingRequest objOTBooking, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TOtbookingRequest objOTBook = await _context.TOtbookingRequests.FindAsync(objOTBooking.OtbookingId);
                 _context.TOtbookingRequests.Update(objOTBooking);
                _context.Entry(objOTBooking).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
