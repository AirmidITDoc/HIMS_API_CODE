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
                    ConfigSetting objConfigRSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));

                    _context.AdvanceHeaders.Add(objAdvanceHeader);
                    await _context.SaveChangesAsync();


                    // Add AdvDetail table records
                       //await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                    advanceDetail.AdvanceId = objAdvanceHeader.AdvanceId;
                    _context.AdvanceDetails.Add(advanceDetail);
                    //_context.ConfigSettings.Update(objConfigRSetting);
                    //_context.Entry(objConfigRSetting).State = EntityState.Modified;
                    await _context.SaveChangesAsync();


                    // Add AdvDetail table records
                      //await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                    objpayment.AdvanceId = objAdvanceHeader.AdvanceId;
                    //_context.ConfigSettings.Update(objConfigRSetting);
                    //_context.Entry(objConfigRSetting).State = EntityState.Modified;
                    _context.Payments.Add(objpayment);

                    await _context.SaveChangesAsync();


                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                AdvanceHeader? objAdvanceHeader1 = await _context.AdvanceHeaders.FindAsync(objAdvanceHeader.AdvanceId);
                _context.AdvanceHeaders.Remove(objAdvanceHeader1);
                await _context.SaveChangesAsync();
            }
        }
    }
}
