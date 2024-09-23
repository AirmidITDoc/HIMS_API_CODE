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

        //    public virtual async Task InsertAsyncSP(IPPrescription objTPrescription, int CurrentUserId, string CurrentUserName)
        //    {
        //        // throw new NotImplementedException();


        //        //PrecriptionId InstructionId Instruction IsEnglishOrIsMarathi Pweight
        //        //Pulse Bp Bsl ChiefComplaint SpO2 DoseOption2 DaysOption2  DoseOption3 DaysOption3


        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {
        //            foreach (var objItem in objTPrescription.TIpPrescription)
        //            {

        //               // _context.objTPrescription.Add(objItem);
        //                await _context.SaveChangesAsync();
        //            }
        //            scope.Complete();
        //        }

        //    }
        
    }
}