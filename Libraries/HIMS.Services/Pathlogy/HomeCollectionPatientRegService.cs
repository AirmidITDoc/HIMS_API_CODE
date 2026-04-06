using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.Pathlogy
{
    public class HomeCollectionPatientRegService : IHomeCollectionPatientRegService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public HomeCollectionPatientRegService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(THomeCollectionPatientRegistartion ObjTHomeCollectPatientRegistartion, int UserId, string Username)

        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            _context.THomeCollectionPatientRegistartions.Add(ObjTHomeCollectPatientRegistartion);
            await _context.SaveChangesAsync();

            scope.Complete();
        }
        

    }
}
