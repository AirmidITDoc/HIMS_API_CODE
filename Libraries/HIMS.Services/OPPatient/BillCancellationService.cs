using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace HIMS.Services.OPPatient
{
    public class BillCancellationService : IBillCancellationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BillCancellationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task UpdateAsyncOp(Bill objOpBillCancellation, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

            string[] AEntity = { "BillNo", "IsCancelledBy", "IsCancelledDatetime" , "CancelReason" };
            var entity = objOpBillCancellation.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_OP_BILL_CANCELLATION", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(Bill), objOpBillCancellation.BillNo.ToInt(), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);
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

        public virtual async Task UpdateAsyncLabBill(Bill objOpBillCancellation, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
            string[] AEntity = { "BillNo", "IsCancelledBy", "CancelReason", "IsCancelledDatetime" };
            var entity = objOpBillCancellation.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_LabBILL_CANCELLATION", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(Bill), objOpBillCancellation.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
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
        public virtual async Task UpdateAsyncIp(Bill objIPBillCancellation, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
            DatabaseHelper odal = new();
            odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
            odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
            string[] AEntity = { "BillNo", "IsCancelledBy", "IsCancelledDatetime" , "CancelReason" };
            var bentity = objIPBillCancellation.ToDictionary();

            foreach (var rProperty in bentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    bentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_IP_BILL_CANCELLATION", CommandType.StoredProcedure, bentity);
            await _context.LogProcedureExecution(bentity, nameof(Bill), objIPBillCancellation.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
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
