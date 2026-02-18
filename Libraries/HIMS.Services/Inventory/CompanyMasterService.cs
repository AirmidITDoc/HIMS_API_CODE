using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
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
        public virtual async Task<IPagedList<ServiceTariffWiseListDto>> SGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ServiceTariffWiseListDto>(model, "ps_Rtrv_ServiceList_TariffWise");
        }
        public virtual async Task<IPagedList<ServiceCompanyTariffWiseListDto>> CGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ServiceCompanyTariffWiseListDto>(model, "ps_Rtrv_ServiceList_CompanyTariffWise");
        }     
        public virtual async Task<IPagedList<CompanyExecutiveInfoListDto>> CompanyExecutiveInfoListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CompanyExecutiveInfoListDto>(model, "ps_Rtrv_getCompanyExecutives");
        }
        
    public List<CompanyMasterDto> CompanyRepresentativeList(long CompanyId)
    {
        DatabaseHelper sql = new();

        SqlParameter[] para =
        {
        new SqlParameter("@CompanyId", CompanyId)
        };

        var data = sql.FetchListBySP<CompanyMasterDto>("Retrieve_companyExecuativeCombo", para);

        return data;
    }
        //public List<CompanyComboDto> CompanyComboList(string keywoard)
        //{
        //    DatabaseHelper sql = new();
        //    SqlParameter[] para =
        //     {
        //     new SqlParameter("@keywoard",string.IsNullOrWhiteSpace(keywoard) ? DBNull.Value : keywoard)
        //     };
        //    var data = sql.FetchListBySP<CompanyComboDto>("ps_Rtrv_CompanyMasterCombo", para);

        //    return data;
        //}
        public virtual void Insertsp(List<ServiceWiseCompanyCode> ObjServiceWiseCompanyCode, int UserId, string UserName, long? userId)
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
        public virtual void Inserts(List<MCompanyWiseServiceDiscount> objCompanyWiseServiceDiscount, int UserId, string UserName, long? userId)
        {
            DatabaseHelper odal = new();

            foreach (var item in objCompanyWiseServiceDiscount)
            {
                string[] Entity = { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "CompServiceDetailId" };
                var entity = item.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    entity.Remove(rProperty);
                }
                entity["userId"] = userId;
                odal.ExecuteNonQuery("ps_insert_update_CompanyWiseServiceDiscount", CommandType.StoredProcedure, entity);

            }
        }

        public virtual async Task InsertAsync(CompanyMaster objCompanyMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.CompanyMasters.Add(objCompanyMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual async Task UpdateAsync(CompanyMaster objCompanyMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                objCompanyMaster.ModifiedBy = UserId;
                objCompanyMaster.ModifiedDate = DateTime.Now;

                _context.CompanyMasters.Attach(objCompanyMaster);
                _context.Entry(objCompanyMaster).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}


