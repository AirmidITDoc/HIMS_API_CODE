using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace HIMS.Services.TrustMembershipRegistration
{
    public class TrustMemberRegService : ITrustMembershipRegService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public TrustMemberRegService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<TrustMembershipRegDTO>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TrustMembershipRegDTO>(model, "ps_Rtrv_TrustMembershipList");
        }


        public virtual async Task<TMembershipRegistration> GetById(int Id)
        {
            return await this._context.TMembershipRegistrations .Include(x => x.TMembershipChildren).Include(x => x.TMembershipEmrgencies).Include(x => x.TMembershipRelatives).FirstOrDefaultAsync(x => x.MembershipId == Id);
        }
        public virtual async Task<List<TrustMembershipRegistrationDTO>> SearchTrust(string str)
        {

            return await this._context.TMembershipRegistrations
                .Where(x =>
                    (x.HusbandFirstName + " " + (x.HusbandMiddleName ?? "") + " " + x.HusbandLastName).ToLower().StartsWith(str) ||
                    x.HusbandFirstName.ToLower().StartsWith(str) ||                    
                                                                                                             
                    x.HusbandMobile.ToLower().Contains(str)
                // Keep full Contains() for MobileNo
                )
                .Take(25)
                .Select(x => new TrustMembershipRegistrationDTO
                {
                    HusbandFirstName = x.HusbandFirstName,
                    MembershipId = x.MembershipId,
                    HusbandLastName = x.HusbandLastName,
                    HusbandMiddleName = x.HusbandMiddleName,
                    HusbandMobile = x.HusbandMobile,
                    MembershipNo = x.MembershipNo,
                    HusbandAgeY = x.HusbandAgeY,
                    HusbandAgeM = x.HusbandAgeM,
                    HusbandAgeD = x.HusbandAgeD,
                    HusbandDob = x.HusbandDob,
                    HusbandEmail = x.HusbandEmail,
                    HusbandAadhaar = x.HusbandAadhaar,
                    CityId = x.CityId,

                })
               .OrderByDescending(x => x.MembershipNo == str ? 3 : x.HusbandMobile == str ? 2 : (x.HusbandFirstName + " " + x.HusbandLastName) == str ? 1 : 0)
               .ThenBy(x => x.HusbandFirstName).ToListAsync();

        }



        public virtual async Task InsertAsync(TMembershipRegistration ObjTMembershipRegistration, int UserId, string Username)
        {
            using var scope = new TransactionScope( TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled);

            var membershipNos = await _context.TMembershipRegistrations
                .Select(x => x.MembershipNo)
                .ToListAsync();

            int lastSeqNo = membershipNos
                .Where(x => !string.IsNullOrWhiteSpace(x) && int.TryParse(x, out _))
                .Select(x => int.Parse(x))
                .DefaultIfEmpty(0)
                .Max();

            // Generate next Membership Number
            ObjTMembershipRegistration.MembershipNo = (lastSeqNo + 1).ToString();

            ObjTMembershipRegistration.CreatedBy = UserId;
            ObjTMembershipRegistration.CreatedDate = AppTime.Now;

            Console.WriteLine("Generated MembershipNo : " + ObjTMembershipRegistration.MembershipNo);

            _context.TMembershipRegistrations.Add(ObjTMembershipRegistration);

            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task UpdateAsync(TMembershipRegistration ObjTMembershipRegistration, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long MembershipId = ObjTMembershipRegistration.MembershipId;

                // Delete related details first
                var lstAttend = await _context.TMembershipChildren
                    .Where(x => x.MembershipId == MembershipId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TMembershipChildren.RemoveRange(lstAttend);

                var lstSurgery = await _context.TMembershipEmrgencies
                    .Where(x => x.MembershipId == MembershipId)
                    .ToListAsync();
                if (lstSurgery.Any())
                    _context.TMembershipEmrgencies.RemoveRange(lstSurgery);

                var lstDiagnosis = await _context.TMembershipRelatives
                    .Where(x => x.MembershipId == MembershipId)
                    .ToListAsync();
                if (lstDiagnosis.Any())
                    _context.TMembershipRelatives.RemoveRange(lstDiagnosis);

                //Save deletion first
                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjTMembershipRegistration);
                _context.Entry(ObjTMembershipRegistration).State = EntityState.Modified;

                _context.Entry(ObjTMembershipRegistration).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTMembershipRegistration).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTMembershipRegistration).Property(x => x.MembershipNo).IsModified = false;

                ObjTMembershipRegistration.ModifiedBy = UserId;
                ObjTMembershipRegistration.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTMembershipRegistration).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
