using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
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
                    (x.FirstName + " " + x.LastName).ToLower().StartsWith(str) || // Optional: if you want full name search
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
                    AadharCardNo= x.AadharCardNo

                })
               .OrderByDescending(x => x.RegNo == str ? 3 : x.MobileNo == str ? 2 : (x.FirstName + " " + x.LastName) == str ? 1 : 0)
               .ThenBy(x => x.FirstName).ToListAsync();

        }


        public virtual async Task InsertAsyncSP(Registration objRegistration, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegDate", "RegTime", "PrefixId", "FirstName", "MiddleName", "LastName", "Address", "City", "PinNo", "DateofBirth", "Age", "GenderId", "PhoneNo", "MobileNo", "AddedBy", "AgeYear", "AgeMonth", "AgeDay", 
                "CountryId", "StateId", "CityId", "MaritalStatusId", "IsCharity", "ReligionId", "AreaId", "IsSeniorCitizen", "AadharCardNo", "PanCardNo", "Photo", "EmgContactPersonName", "EmgRelationshipId", "EmgMobileNo", 
                "EmgLandlineNo", "EngAddress", "EmgAadharCardNo", "EmgDrivingLicenceNo", "MedTourismPassportNo", "MedTourismVisaIssueDate", "MedTourismVisaValidityDate", "MedTourismNationalityId", "MedTourismCitizenship", "MedTourismPortOfEntry", 
                "MedTourismDateOfEntry", "MedTourismResidentialAddress", "MedTourismOfficeWorkAddress","EmailId", "RegId" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);
            await _context.LogProcedureExecution(entity, nameof(Registration), objRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
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

        public virtual async Task UpdateAsync(Registration objRegistration, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                Registration objReg = _context.Registrations.Where(x => x.RegId == objRegistration.RegId).FirstOrDefault();
                if (objReg != null)
                    _context.Entry(objReg).State = EntityState.Detached;

                objRegistration.RegNo = objReg.RegNo;
                objRegistration.RegPrefix = objReg.RegPrefix;
                objRegistration.AddedBy = objReg.AddedBy;
                objRegistration.UpdatedBy = objReg.UpdatedBy;
                objRegistration.AnnualIncome = objReg.AnnualIncome;
                objRegistration.IsIndientOrWeaker = objReg.IsIndientOrWeaker;
                objRegistration.RationCardNo = objReg.RationCardNo;
                objRegistration.IsMember = objReg.IsMember;
                objRegistration.RegDate = objReg.RegDate;
                objRegistration.RegTime = objReg.RegTime;
                _context.Registrations.Update(objRegistration);
                _context.Entry(objRegistration).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task RegUpdateAsync(Registration ObjRegistration, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "RegId", "AadharCardNo", "MobileNo", "EmailId" };
            var Rentity = ObjRegistration.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_RegistrationUpdate", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(Registration), ObjRegistration.RegId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }


    }
}
