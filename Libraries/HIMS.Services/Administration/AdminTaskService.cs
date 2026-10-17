using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public class AdminTaskService : IAdminTaskService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AdminTaskService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task BilldateUpdateAsync(Bill ObjBill, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            string[] AEntity = { "BillNo", "BillDate", "BillTime" };
            var Rentity = ObjBill.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_Administrtaion_BillDate", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution( Rentity, nameof(Bill), (int)ObjBill.BillNo, Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            // Save & Commit
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
            await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task  Update(Admission ObjAdmission, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
             DatabaseHelper odal = new();
             odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
             odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            string[] AEntity = { "AdmissionId", "AdmissionDate", "AdmissionTime", "Ipdno" };
            var Rentity = ObjAdmission.ToDictionary();
            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Update_AdmissionDateTime", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(Admission), (int)ObjAdmission.AdmissionId,Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
            // Save & Commit
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
            await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

    }
}
