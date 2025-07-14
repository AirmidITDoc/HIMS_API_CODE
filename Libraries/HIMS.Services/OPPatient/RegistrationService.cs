using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
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
                    x.MobileNo.ToLower().Contains(str)                           // Keep full Contains() for MobileNo
                )
                .Take(25)
                .Select(x => new RegistrationAutoCompleteDto
                {
                    FirstName = x.FirstName,
                    Id = x.RegId,
                    LastName = x.LastName,
                    Mobile = x.MobileNo,
                    RegNo = x.RegNo,
                    MobileNo = x.MobileNo,
                    AgeYear = x.AgeYear,
                    AgeMonth = x.AgeMonth,
                    AgeDay = x.AgeDay

                })
               .OrderByDescending(x => x.RegNo == str ? 3 : x.MobileNo == str ? 2 : (x.FirstName + " " + x.LastName) == str ? 1 : 0)
               .ThenBy(x => x.FirstName).ToListAsync();

        }


        public virtual async Task InsertAsyncSP(Registration objRegistration, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegNo", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember", "UpdatedBy" ,"CreatedBy", "CreatedDate", "ModifiedBy","ModifiedDate" };
            var entity = objRegistration.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RegId = odal.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
            objRegistration.RegId = Convert.ToInt32(RegId);

            await _context.SaveChangesAsync(UserId, Username);
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
       
    }
}
