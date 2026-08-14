using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public class ClassMasterService : IClassMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ClassMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(ServiceDetail ObjServiceDetail, int CurrentUserId, string CurrentUserName, int OldClassId, int NewClassId)
        {

            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                string[] Entity = { "TariffId", "OldClassId", "NewClassId" };
                var entity = ObjServiceDetail.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                entity["OldClassId"] = OldClassId;
                entity["NewClassId"] = NewClassId;
                odal.ExecuteNonQuery("ps_ApplyNewClassToAllServices", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(ServiceDetail), ObjServiceDetail.ServiceDetailId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                // Save Log
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                // Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
         


