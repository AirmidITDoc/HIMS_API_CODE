using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Transactions;

namespace HIMS.Services.OPPatient
{
    public class RegistrationService : IRegistrationService
    {
        private readonly HIMSDbContext _context;
        public RegistrationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<RegistrationListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RegistrationListDto>(model, "ps_Rtrv_RegistrationList");

        }
        public virtual async Task<List<RegistrationAutoCompleteDto>> SearchRegistration(string str)
        {

            return await this._context.Registrations
                .Where(x =>
                    //(x.FirstName + " " + x.LastName).ToLower().StartsWith(str) || // Optional: if you want full name search
                    (x.FirstName + " " + (x.MiddleName ?? "") + " " + x.LastName) .ToLower() .StartsWith(str) ||
                    x.FirstName.ToLower().StartsWith(str) ||                     // Match first name starting with str
                    x.RegNo.ToLower().StartsWith(str) ||                         // Match RegNo starting with str
                    x.MobileNo.ToLower().Contains(str) 
                                                                                // Keep full Contains() for MobileNo
                )
                .Take(25)
                .Select(x => new RegistrationAutoCompleteDto
                {
                    FirstName = x.FirstName,
                    Id = x.RegId,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName,
                    Mobile = x.MobileNo,
                    RegNo = x.RegNo,
                    MobileNo = x.MobileNo,
                    AgeYear = x.AgeYear,
                    AgeMonth = x.AgeMonth,
                    AgeDay = x.AgeDay,
                    DateofBirth = x.DateofBirth,
                    EmailId =  x.EmailId,
                    RegId =x.RegId,
                    AadharCardNo= x.AadharCardNo,
                    GenderId = x.GenderId,
                    AbhaTranId = x.AbhaTranId

                })
               .OrderByDescending(x => x.RegNo == str ? 3 : x.MobileNo == str ? 2 : (x.FirstName + " " + x.LastName) == str ? 1 : 0)
               .ThenBy(x => x.FirstName).ToListAsync();

        }


        public virtual async Task InsertAsyncSP(Registration objRegistration, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] rEntity = { "RegDate", "RegTime", "PrefixId", "FirstName", "MiddleName", "LastName", "Address", "City", "PinNo", "DateofBirth", "Age", "GenderId", "PhoneNo", "MobileNo", "AddedBy", "AgeYear", "AgeMonth", "AgeDay",
                "CountryId", "StateId", "CityId", "MaritalStatusId", "IsCharity", "ReligionId", "AreaId", "IsSeniorCitizen", "AadharCardNo", "PanCardNo", "Photo", "EmgContactPersonName", "EmgRelationshipId", "EmgMobileNo",
                "EmgLandlineNo", "EngAddress", "EmgAadharCardNo", "EmgDrivingLicenceNo", "MedTourismPassportNo", "MedTourismVisaIssueDate", "MedTourismVisaValidityDate", "MedTourismNationalityId", "MedTourismCitizenship", "MedTourismPortOfEntry",
                "MedTourismDateOfEntry", "MedTourismResidentialAddress", "MedTourismOfficeWorkAddress","EmailId","MembershipId", "RegId","tPatientAbhaInformations" };
                var entity = objRegistration.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                string RegId = odal.ExecuteNonQueryNew("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
                objRegistration.RegId = Convert.ToInt32(RegId);
                objRegistration.TPatientAbhaInformations.First().RegId = Convert.ToInt32(RegId);
                await _context.LogProcedureExecution(entity, nameof(Registration), objRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                string[] PatientPolicyEntity = { "RegId", "AbhaNumber", "AbhaFullName", "AbhaAddress", "Gender", "YearOfBirth", "Verified", "VerifiedDateTime", "AbhaTranId", "CreatedBy" };

                var abhaList = objRegistration.TPatientAbhaInformations?.Where(x =>
                        !string.IsNullOrWhiteSpace(x.AbhaNumber) ||
                        !string.IsNullOrWhiteSpace(x.AbhaFullName) ||
                        !string.IsNullOrWhiteSpace(x.AbhaAddress))
                    .ToList();

                if (abhaList?.Any() == true)
                {
                    foreach (var abhaInfo in objRegistration.TPatientAbhaInformations)
                    {
                        var Patiententity = abhaInfo.ToDictionary();

                        foreach (var rProperty in Patiententity.Keys.ToList())
                        {
                            if (!PatientPolicyEntity.Contains(rProperty))
                                Patiententity.Remove(rProperty);
                        }
                        string AbhaTranId = odal.ExecuteNonQueryNew("ps_Insert_PatientAbhaInformation", CommandType.StoredProcedure, "AbhaTranId", Patiententity);
                        await _context.LogProcedureExecution(Patiententity, nameof(TPatientAbhaInformation), objRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                    }
                }

                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task InsertAsync(Registration objregistration, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.Registrations.Add(objregistration);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsyncSP(Registration objRegistration, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] rEntity = { "RegId", "PrefixId", "FirstName", "MiddleName", "LastName", "Address", "AadharCardNo", "GenderId", "DateofBirth", "Age", "AgeYear", "AgeMonth", "AgeDay", "PhoneNo", "MobileNo", "PanCardNo", "MaritalStatusId", "ReligionId", 
                    "AreaId", "CityId", "City", "StateId", "CountryId", "IsCharity", "IsSeniorCitizen", "Photo", "PinNo", "EmgContactPersonName", "EmgRelationshipId", "EmgMobileNo", "EmgLandlineNo", "EngAddress", "EmgAadharCardNo", 
                    "EmgDrivingLicenceNo", "MedTourismPassportNo", "MedTourismVisaIssueDate", "MedTourismVisaValidityDate", "MedTourismNationalityId", "MedTourismCitizenship", "MedTourismPortOfEntry", "MedTourismDateOfEntry", 
                    "MedTourismResidentialAddress", "MedTourismOfficeWorkAddress", "EmailId", "MembershipId" ,"tPatientAbhaInformations"};
                var entity = objRegistration.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                string RegId = odal.ExecuteNonQueryNew("ps_Update_Registration", CommandType.StoredProcedure, "", entity);
                await _context.LogProcedureExecution(entity, nameof(Registration), objRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                string[] PatientPolicyEntity = { "RegId", "AbhaNumber", "AbhaFullName", "AbhaAddress", "Gender", "YearOfBirth", "Verified", "VerifiedDateTime", "AbhaTranId", "CreatedBy" };

                var abhaList = objRegistration.TPatientAbhaInformations?.Where(x =>
                       !string.IsNullOrWhiteSpace(x.AbhaNumber) ||
                       !string.IsNullOrWhiteSpace(x.AbhaFullName) ||
                       !string.IsNullOrWhiteSpace(x.AbhaAddress))
                   .ToList();

                if (abhaList?.Any() == true)
                {
                    foreach (var abhaInfo in objRegistration.TPatientAbhaInformations)
                    {
                        var Patiententity = abhaInfo.ToDictionary();

                        foreach (var rProperty in Patiententity.Keys.ToList())
                        {
                            if (!PatientPolicyEntity.Contains(rProperty))
                                Patiententity.Remove(rProperty);
                        }
                        string AbhaTranId = odal.ExecuteNonQueryNew("ps_Insert_PatientAbhaInformation", CommandType.StoredProcedure, "AbhaTranId", Patiententity);
                        await _context.LogProcedureExecution(Patiententity, nameof(TPatientAbhaInformation), objRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                    }
                }
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task UpdateAsync(Registration objRegistration, int userId, string username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,new TransactionOptions{IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted},TransactionScopeAsyncFlowOption.Enabled);

            var objReg = await _context.Registrations.FirstOrDefaultAsync(x => x.RegId == objRegistration.RegId);
            //var objReg = await _context.Registrations.Include(x => x.TPatientAbhaInformations).FirstOrDefaultAsync(x => x.RegId == objRegistration.RegId);

            if (objReg == null)
                throw new Exception("Registration not found.");

            _context.Entry(objReg).State = EntityState.Detached;

            // Preserve old values
            objRegistration.RegNo = objReg.RegNo;
            objRegistration.RegPrefix = objReg.RegPrefix;
            objRegistration.AddedBy = objReg.AddedBy;
            objRegistration.CreatedBy = objReg.CreatedBy;
            objRegistration.CreatedDate = objReg.CreatedDate;
            objRegistration.RegDate = objReg.RegDate;
            objRegistration.RegTime = objReg.RegTime;

            // Update audit fields
            objRegistration.UpdatedBy = userId;
            //objRegistration.UpdatedDate = DateTime.Now;

            //_context.Registrations.Update(objRegistration);

            // Don't update created fields
            _context.Entry(objRegistration).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(objRegistration).Property(x => x.CreatedDate).IsModified = false;


            //var deleted = objReg.TPatientAbhaInformations.Where(x => objRegistration.TPatientAbhaInformations.Any(y => y.RegId == x.RegId)).ToList();
            //_context.TPatientAbhaInformations.RemoveRange(deleted);

            foreach (var item in objRegistration.TPatientAbhaInformations)
            {
                Console.WriteLine(objReg.TPatientAbhaInformations?.Count);
                var existing = objReg.TPatientAbhaInformations.FirstOrDefault(x => x.AbhaNumber == item.AbhaNumber);
                
                item.CreatedBy = 2; //userId;
                item.CreatedDate = DateTime.Now; 

                if (existing == null)
                {
                    // Insert
                    objReg.TPatientAbhaInformations.Add(item);
                }
                else
                {
                    // Update
                    _context.Entry(existing).CurrentValues.SetValues(item);
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task RegUpdateAsync(Registration ObjRegistration, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] AEntity = { "RegId", "AadharCardNo", "MobileNo", "EmailId" };
                var Rentity = ObjRegistration.ToDictionary();

                foreach (var rProperty in Rentity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        Rentity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew("ps_RegistrationUpdate", CommandType.StoredProcedure, "", Rentity);
                await _context.LogProcedureExecution(Rentity, nameof(Registration), ObjRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                // SaveChanges (important)
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
