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
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.Design;
using System.Data;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



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

            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
            scope.Complete();
        }
        public virtual async Task Inserts(List<MCompanyWiseServiceDiscount> objCompanyWiseServiceDiscount, int CurrentUserId, string CurrentUserName, long? userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // OLD CODE With SP
                DatabaseHelper odal = new();

                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                foreach (var item in objCompanyWiseServiceDiscount)
                {
                    string[] Entity = { "IsGroupOrSubGroup", "ServiceId", "TariffId", "ClassId", "DiscountAmount", "DiscountPercentage", "userId" };
                    var entity = item.ToDictionary();
                    foreach (var rProperty in entity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            entity.Remove(rProperty);
                    }
                    entity["userId"] = userId;
                    odal.ExecuteNonQuery("ps_insert_update_CompanyWiseServiceDiscount", CommandType.StoredProcedure, entity);
                    await _context.LogProcedureExecution(entity.ToDictionary(), nameof(MCompanyWiseServiceDiscount), (int?)item.CompServiceDetailId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                }

                // Save & Commit
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task InsertAsync(CompanyMaster objCompanyMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.CompanyMasters.Add(objCompanyMaster);
                await _context.SaveChangesAsync(UserId, Username);
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

                await _context.SaveChangesAsync(UserId, Username);
                scope.Complete();
            }
        }
    }
}


