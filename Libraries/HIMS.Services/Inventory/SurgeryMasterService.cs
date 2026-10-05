using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using LinqToDB.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;


namespace HIMS.Services.Inventory
{
    public class SurgeryMasterService : ISurgeryMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SurgeryMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SurgeryMasterListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SurgeryMasterListDto>(model, "ps_SurgeryMasterList");
        }

        public List<MOtSurgeryMaster> GetSurgeryNameBySurgeryType(int SiteDescId)
        {
            DatabaseHelper sql = new();

            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@SiteDescId", SiteDescId);

            return sql.FetchListByQuery<MOtSurgeryMaster>("EXEC ps_RrvGetSurgeryNameBySurgeryType @SiteDescId", para);
        }

        public virtual async Task InsertAsync( MOtSurgeryMaster ObjMOtSurgeryMaster, int UserId,string Username)
        {
            using var scope = new TransactionScope(  TransactionScopeOption.Required, new TransactionOptions{ IsolationLevel = System.Transactions.IsolationLevel.Serializable},  TransactionScopeAsyncFlowOption.Enabled);

            const string prefix = "SC";

            // Get existing Surgery Codes
            var surgeryCodes = await _context.MOtSurgeryMasters
                .AsNoTracking()
                .Where(x => !string.IsNullOrEmpty(x.SurgeryCode)
                            && x.SurgeryCode.StartsWith(prefix))
                .Select(x => x.SurgeryCode)
                .ToListAsync();

            // Get the last sequence number
            int lastSeqNo = surgeryCodes
                .Select(x =>
                    int.TryParse(x.Substring(prefix.Length), out int number) ? number  : 0)
                .DefaultIfEmpty(0)
                .Max();

            // Generate new Surgery Code
            int newSeqNo = lastSeqNo + 1;

            ObjMOtSurgeryMaster.SurgeryCode = prefix + newSeqNo.ToString("D2");

            // Set Created details
            ObjMOtSurgeryMaster.CreatedBy = UserId;
            ObjMOtSurgeryMaster.CreatedDate = AppTime.Now;

            // Insert record
            _context.MOtSurgeryMasters.Add(ObjMOtSurgeryMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task UpdateAsync(MOtSurgeryMaster ObjMOtSurgeryMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            _context.Attach(ObjMOtSurgeryMaster);
            _context.Entry(ObjMOtSurgeryMaster).State = EntityState.Modified;

            _context.Entry(ObjMOtSurgeryMaster).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(ObjMOtSurgeryMaster).Property(x => x.CreatedDate).IsModified = false;
            _context.Entry(ObjMOtSurgeryMaster).Property(x => x.SurgeryCode).IsModified = false;

            ObjMOtSurgeryMaster.ModifiedBy = UserId;
            ObjMOtSurgeryMaster.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(ObjMOtSurgeryMaster).Property(column).IsModified = false;
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }

    }
}
