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

namespace HIMS.Services.OutPatient
{
    public class PatientAbhaInformationService : IPatientAbhaInformationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PatientAbhaInformationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TPatientAbhaInformation ObjTPatientAbhaInformation, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] Entity = { "RegId", "AbhaNumber", "AbhaFullName", "AbhaAddress", "Gender", "YearOfBirth", "Verified", "VerifiedDateTime", "CreatedBy" };

                var entity = ObjTPatientAbhaInformation.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                string AbhaTranId = odal.ExecuteNonQueryNew("ps_Insert_PatientAbhaInformation", CommandType.StoredProcedure, "AbhaTranId", entity);
                ObjTPatientAbhaInformation.AbhaTranId = Convert.ToInt32(AbhaTranId);
                await _context.LogProcedureExecution(entity, nameof(TPatientAbhaInformation), Convert.ToInt32(ObjTPatientAbhaInformation.AbhaTranId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                //Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }
            public virtual async Task UpdateAsync(TPatientAbhaInformation objTPatientAbhaInformation, int CurrentUserId, string CurrentUserName)
            {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] Entity ={"AbhaTranId",  "RegId",  "AbhaNumber",  "AbhaFullName",  "AbhaAddress",  "Gender",  "YearOfBirth",  "Verified",  "VerifiedDateTime",  "ModifiedBy" };

                var entity = objTPatientAbhaInformation.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew(  "ps_Update_PatientAbhaInformation",  CommandType.StoredProcedure, "",entity);

                await _context.LogProcedureExecution(entity, nameof(TPatientAbhaInformation),  Convert.ToInt32(objTPatientAbhaInformation.AbhaTranId),  Core.Domain.Logging.LogAction.Edit,  CurrentUserId,  CurrentUserName);

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



