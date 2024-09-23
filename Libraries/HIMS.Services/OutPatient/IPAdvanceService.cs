using Aspose.Cells.Drawing;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public class IPAdvanceService:IIPAdvanceService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPAdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail advanceDetail, Payment objpayment, int CurrentUserId, string CurrentUserName)
        {
            try
            {
              
                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {

                    //AdHeader
                    _context.AdvanceHeaders.Add(objAdvanceHeader);
                    await _context.SaveChangesAsync();


                    // Add AdvDetail table records
                    advanceDetail.AdvanceId = objAdvanceHeader.AdvanceId;
                    _context.AdvanceDetails.Add(advanceDetail);
                    await _context.SaveChangesAsync();


                    // Add AdvDetail table records
                    objpayment.AdvanceId = objAdvanceHeader.AdvanceId;
                    _context.Payments.Add(objpayment);
                    await _context.SaveChangesAsync();


                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                //Bill? objAdvanceHeader = await _context.Bills.FindAsync(objAdvanceHeader.BillNo);
                //_context.Bills.Remove(objBills);
                //await _context.SaveChangesAsync();
            }
        }
    }
}
