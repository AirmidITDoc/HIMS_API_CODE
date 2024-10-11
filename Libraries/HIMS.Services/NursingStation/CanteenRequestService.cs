using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.NursingStation
{
    public class CanteenRequestService: ICanteenRequestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CanteenRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TCanteenRequestHeader objCanteenRequestHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TCanteenRequestHeaders.Add(objCanteenRequestHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
