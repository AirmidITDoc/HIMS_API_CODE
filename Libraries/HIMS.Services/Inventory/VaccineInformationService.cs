using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class VaccineInformationService : IVaccineInformationService
    {
        private readonly HIMSDbContext _context;

        public VaccineInformationService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task InsertAsync(TVaccineInformation objVaccineInfo, int currentUserId, string currentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.Serializable
                },
                TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "VC";

            var vaccineCodes = await _context.TVaccineInformations
                .Where(x => !string.IsNullOrEmpty(x.VaccineCode) && x.VaccineCode.StartsWith(prefix))
                .Select(x => x.VaccineCode)
                .ToListAsync();

            int lastSeqNo = vaccineCodes
                .Select(x => int.TryParse(x.Substring(prefix.Length), out int number) ? number : 0)
                .DefaultIfEmpty(0)
                .Max();

            int newSeqNo = lastSeqNo + 1;

            objVaccineInfo.VaccineCode = prefix + newSeqNo.ToString("D3");

           
            objVaccineInfo.CreatedBy = currentUserId;
            objVaccineInfo.CreatedDate = AppTime.Now;
            objVaccineInfo.ModifiedBy = currentUserId;
            objVaccineInfo.ModifiedDate = AppTime.Now;

            _context.TVaccineInformations.Add(objVaccineInfo);
            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(TVaccineInformation objVaccineInfo, int userId, string userName, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(objVaccineInfo);
            _context.Entry(objVaccineInfo).State = EntityState.Modified;

            _context.Entry(objVaccineInfo).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(objVaccineInfo).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(objVaccineInfo).Property(x => x.VaccineCode).IsModified = false;

            objVaccineInfo.ModifiedBy = userId;
            objVaccineInfo.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                {
                    _context.Entry(objVaccineInfo)
                        .Property(column)
                        .IsModified = false;
                }
            }

            await _context.SaveChangesAsync();
            scope.Complete();
        }
    }
}