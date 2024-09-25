using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class PrescriptionSer : IPrescription
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PrescriptionSer(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(TPrescription objPrescription, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "DoseOption2", "DaysOption2", "DoseOption3", "DaysOption3", "IsAddBy", "SpO2", "ChiefComplaint" };
            var entity = objPrescription.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PrecriptionId = odal.ExecuteNonQuery("insert_Prescription_1", CommandType.StoredProcedure, "PrecriptionId", entity);
            objPrescription.PrecriptionId = Convert.ToInt32(PrecriptionId);

            await _context.SaveChangesAsync(UserId, Username);
        }



        public virtual async Task InsertAsync(TPrescription objPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TPrescriptions.Add(objPrescription);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }



        public virtual async Task UpdateAsync(TPrescription objPrescription, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TPrescriptions.Update(objPrescription);
                _context.Entry(objPrescription).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

       
    }
}
