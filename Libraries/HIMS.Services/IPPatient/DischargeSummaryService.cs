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

namespace HIMS.Services.IPPatient
{
    public class DischargeSummaryService : IDischargeSummaryService
    {
        private readonly HIMSDbContext _context;
        public DischargeSummaryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(DischargeSummary OBJDischargeSummary, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.DischargeSummaries.Add(OBJDischargeSummary);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
       

        public virtual async Task InsertAsyncSP(DischargeSummary OBJDischargeSummary, TIpPrescriptionDischarge OBJTIpPrescriptionDischarge, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "AddedByDate", "UpdatedBy", "UpdatedByDate" };
            var entity = OBJDischargeSummary.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VDischargeSummaryId = odal.ExecuteNonQuery("sp_insert_DischargeSummary_1", CommandType.StoredProcedure, "DischargeSummaryId", entity);
            OBJDischargeSummary.DischargeSummaryId = Convert.ToInt32(VDischargeSummaryId);

            

                string[] DEntity = { "PrecriptionId", "CreatedDate", "ModifiedBy", "ModifiedDate", "IsClosed" };
                var pentity = OBJTIpPrescriptionDischarge.ToDictionary();
                foreach (var Property in DEntity)
                {
                    pentity.Remove(Property);
                }
                odal.ExecuteNonQuery("ps_insert_T_IP_Prescription_Discharge_1", CommandType.StoredProcedure, pentity);
            
        }









        public virtual async Task UpdateAsync(DischargeSummary OBJDischargeSummary, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                //Update header &detail table records
                _context.DischargeSummaries.Update(OBJDischargeSummary);
                _context.Entry(OBJDischargeSummary).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }

        }
    }
}
