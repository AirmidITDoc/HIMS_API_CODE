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
    public class IPPrescriptionService : IIPPrescriptionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPPrescriptionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(TIpPrescription objTPrescription, int CurrentUserId, string CurrentUserName)
        {
            // throw new NotImplementedException();
            //insert_T_IPMedicalRecord_1
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                //foreach (var objItem in objTPrescription.TIpPrescription)
                //{
                   // _context.TIpPrescription.Add(objTPrescription);
                    await _context.SaveChangesAsync();
                //}
                scope.Complete();
            }

        }

    }
}