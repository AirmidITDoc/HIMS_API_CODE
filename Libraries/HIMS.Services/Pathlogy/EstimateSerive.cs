using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace HIMS.Services.Pathlogy
{
    public class EstimateSerive : IEstimasteService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public EstimateSerive(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<EstimateListDto>> EstimateListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EstimateListDto>(model, "ps_rpt_EstimateList");
        }

        public virtual async Task<IPagedList<EstimateDetailsListDto>> EstimateDetailsListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EstimateDetailsListDto>(model, "ps_rpt_EstimateDetailsList");
        }
        public virtual async Task InsertAsync(TEstimateHeader ObjTEstimateHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TEstimateHeaders
                    .OrderByDescending(x => x.EstimateNo)
                    .Select(x => x.EstimateNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTEstimateHeader.EstimateNo = newSeqNo.ToString();
                ObjTEstimateHeader.CreatedBy = UserId;
                ObjTEstimateHeader.CreatedDate = AppTime.Now;

                _context.TEstimateHeaders.Add(ObjTEstimateHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }      
        }
    }
}
