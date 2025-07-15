using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services.Inventory
{
    public class CompanyMasterService : ICompanyMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CompanyMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<CompanyMasterListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CompanyMasterListDto>(model, "PS_Rtrv_CompanyMasterList");
        }
        public virtual async Task UpdateAsync(ServiceDetail objServiceDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            var existing = await _context.ServiceDetails.FirstOrDefaultAsync(x => x.ServiceDetailId == objServiceDetail.ServiceDetailId && x.ServiceId == objServiceDetail.ServiceId);


            //  Update only the required fields
            existing.ClassRate = objServiceDetail.ClassRate;
            existing.DiscountAmount = objServiceDetail.DiscountAmount;
            existing.DiscountPercentage = objServiceDetail.DiscountPercentage;

            await _context.SaveChangesAsync();
            scope.Complete();
        }




    }
}

    
