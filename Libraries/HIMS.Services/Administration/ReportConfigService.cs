using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System.Transactions;

namespace HIMS.Services.Administration
{
    public class ReportConfigService : IReportConfigService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ReportConfigService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;

        }
        public virtual async Task<IPagedList<MReportConfigListDto>> MReportConfigList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MReportConfigListDto>(model, "ps_ReportConfigList");
        }


        public virtual async Task InsertAsyncm(MReportConfig ObjMReportConfig, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MReportConfigs.Add(ObjMReportConfig);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsyncm(MReportConfig ObjMReportConfig, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var lst = await _context.MReportConfigDetails.Where(x => x.ReportId == ObjMReportConfig.ReportId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MReportConfigDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
                // Update header & detail table records
                _context.MReportConfigs.Update(ObjMReportConfig);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


    }
}
