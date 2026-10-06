using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
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
        public virtual async Task<List<SurgeryListDto>> GetServiceListwithSurgeryWise( int TariffId, int ClassId, bool IsProcedure, string ServiceName)
        {
            // If ServiceName is "%" (wildcard), set it to null
            if (ServiceName == "%")
            {
                ServiceName = null;
            }

            var query = _context.ServiceMasters
                .Join( _context.GroupMasters,service => service.GroupId,group => group.GroupId,(service, group) => new { service, group } )
                .Join(_context.ServiceDetails, sg => sg.service.ServiceId, detail => detail.ServiceId, (sg, detail) => new { sg.service,  sg.group, detail} )
                .Where(x =>
                    (string.IsNullOrEmpty(ServiceName) ||
                     x.service.ServiceName.Contains(ServiceName))
                    && x.detail.TariffId == TariffId
                    && x.detail.ClassId == ClassId
                    && x.service.IsActive == true
                    && x.service.IsProcedure == IsProcedure
                )
                .Select(x => new SurgeryListDto
                {
                    ServiceId = x.service.ServiceId,
                    ServiceName = x.service.ServiceName,
                    Price = x.detail.ClassRate,
                    IsPathology = x.service.IsPathology,
                    IsRadiology = x.service.IsRadiology,
                    IsProcedure = x.service.IsProcedure,
                    TariffId = x.detail.TariffId
                });

            return await query.ToListAsync();
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
