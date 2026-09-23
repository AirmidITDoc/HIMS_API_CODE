using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietkitchenMaster
{
    public class AllergyMasterService : IAllergyMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AllergyMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(MAllergyMaster ObjMAllergyMaster, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.Serializable
                },
                TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "ALG";

            var allergyCodes = await _context.MAllergyMasters
                .Where(x => !string.IsNullOrEmpty(x.AllergyCode) && x.AllergyCode.StartsWith(prefix))
                .Select(x => x.AllergyCode)
                .ToListAsync();

            int lastSeqNo = allergyCodes
                .Select(x => int.TryParse(x.Substring(prefix.Length), out int number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;

            ObjMAllergyMaster.AllergyCode = prefix + newSeqNo.ToString("D3");

            ObjMAllergyMaster.CreatedBy = CurrentUserId;
            ObjMAllergyMaster.CreatedDate = AppTime.Now;
            ObjMAllergyMaster.ModifiedBy = CurrentUserId;
            ObjMAllergyMaster.ModifiedDate = AppTime.Now;

            _context.MAllergyMasters.Add(ObjMAllergyMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(MAllergyMaster ObjMAllergyMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjMAllergyMaster);
            _context.Entry(ObjMAllergyMaster).State = EntityState.Modified;

            _context.Entry(ObjMAllergyMaster).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(ObjMAllergyMaster).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(ObjMAllergyMaster).Property(x => x.AllergyCode).IsModified = false;

            ObjMAllergyMaster.ModifiedBy = UserId;
            ObjMAllergyMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(ObjMAllergyMaster)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}