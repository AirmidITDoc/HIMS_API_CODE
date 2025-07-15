using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;


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

        public virtual async Task InsertAsyncsp(List<ServiceWiseCompanyCode> ObjServiceWiseCompanyCode, int UserId, string UserName, long? userId)
        {
            DatabaseHelper odal = new();

            foreach (var item in ObjServiceWiseCompanyCode)
            {
                string[] AEntity = { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "ServiceDetCompId" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in AEntity)
                {
                    Pentity.Remove(rProperty);
                }
                Pentity["userId"] = userId;
                odal.ExecuteNonQuery("ps_insert_update_ServiceWiseCompany", CommandType.StoredProcedure, Pentity);

            }
        }

        public virtual async Task UpdateAsync(ServiceDetail objServiceDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            var existing = await _context.ServiceDetails.FirstOrDefaultAsync(x => x.ServiceId == objServiceDetail.ServiceId && x.TariffId == objServiceDetail.TariffId && x.ClassId == objServiceDetail.ClassId);



            //  Update only the required fields
            existing.ClassRate = objServiceDetail.ClassRate;
            existing.DiscountAmount = objServiceDetail.DiscountAmount;
            existing.DiscountPercentage = objServiceDetail.DiscountPercentage;

            await _context.SaveChangesAsync();
            scope.Complete();
        }



    }
}


