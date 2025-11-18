using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class BillingService : IBillingService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BillingService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<BillingServiceDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BillingServiceDto>(model, "ps_Rtrv_ServiceList_Pagn");
        }
        public virtual async Task<IPagedList<PackageDetListDto>> GetListAsyncD(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PackageDetListDto>(model, "m_Retrieve_PackageDetails");
        }
        public virtual async Task<IPagedList<PackageServiceInfoListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PackageServiceInfoListDto>(model, "ps_Rtrv_PackageServiceInfo");
        }
        public virtual async Task<IPagedList<PackageServiceInfoListDto>> ListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PackageServiceInfoListDto>(model, "m_Rtrv_ServiceClassdetail");
        }

        public virtual async Task InsertAsync(ServiceMaster objService, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.ServiceMasters.Add(objService);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(ServiceMaster objService, int UserId, string Username, string[]? ignoreColumns = null)
        {
            //using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            //{

            //    _context.Entry(objService).State = EntityState.Modified;
            //    if ((ignoreColumns?.Length ?? 0) > 0)
            //    {
            //        foreach (var column in ignoreColumns)
            //        {
            //            _context.Entry(objService).Property(column).IsModified = false;
            //        }
            //    }

            //    // Delete details table realted records
            //    var lst = await _context.ServiceDetails.Where(x => x.ServiceId == objService.ServiceId).ToListAsync();
            //    if (lst.Count > 0)
            //    {
            //        _context.ServiceDetails.RemoveRange(lst);
            //    }
            //    await _context.SaveChangesAsync();
            //    // Update header & detail table records
            //    _context.ServiceMasters.Update(objService);
            //    _context.Entry(objService).State = EntityState.Modified;
            //    await _context.SaveChangesAsync();
            //    scope.Complete();
            //}


            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // 1. Attach the entity without marking everything as modified
                _context.Attach(objService);
                _context.Entry(objService).State = EntityState.Modified;

                // 2. Ignore specific columns
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(objService).Property(column).IsModified = false;
                    }
                }

                // 3. Delete details related to the service
                var lst = await _context.ServiceDetails.Where(x => x.ServiceId == objService.ServiceId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.ServiceDetails.RemoveRange(lst);
                }

                // 4. Save changes once
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task CancelAsync(ServiceMaster objService, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                ServiceMaster objBilling = await _context.ServiceMasters.FindAsync(objService.ServiceId);
                objBilling.IsActive = false;
                objBilling.CreatedDate = objService.CreatedDate;
                objBilling.CreatedBy = objService.CreatedBy;
                _context.ServiceMasters.Update(objBilling);
                _context.Entry(objBilling).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task<List<ServiceMaster>> GetAllRadiologyTest()
        {
            var query = from M in _context.ServiceMasters
                        where M.IsActive == true
                        orderby M.ServiceId
                        select M;

            return await query.ToListAsync();
        }


        public virtual async Task<List<BillingServiceListDto>> GetServiceListwithGroupWise(int TariffId, int ClassId, string isPathRad, string ServiceName)
        {
            // If serviceName is '%' (wildcard), set it to null for filtering
            if (ServiceName == "%")
            {
                ServiceName = null;
            }

            // Start constructing the query
            var query = _context.ServiceMasters
                .Join(_context.GroupMasters, service => service.GroupId, group => group.GroupId, (service, group) => new { service, group })
                .Join(_context.ServiceDetails, sg => sg.service.ServiceId, detail => detail.ServiceId, (sg, detail) => new { sg.service, sg.group, detail })
                .Where(x => (string.IsNullOrEmpty(ServiceName) || x.service.ServiceName.Contains(ServiceName)) // Handle ServiceName condition
                            && x.detail.TariffId == TariffId
                            && x.detail.ClassId == ClassId
                            && x.service.IsActive == true) // Only active services
                .Select(x => new BillingServiceListDto
                {
                    ServiceId = x.service.ServiceId,
                    ServiceName = x.service.ServiceName,
                    Price = x.detail.ClassRate,
                    IsPathology = x.service.IsPathology,
                    IsRadiology = x.service.IsRadiology,
                    TariffId = x.detail.TariffId
                });

            // Filter based on IsPathRad value
            if (isPathRad == "1") // Pathology
            {
                query = query.Where(x => x.IsPathology == 1);
            }
            else if (isPathRad == "2") // Radiology
            {
                query = query.Where(x => x.IsRadiology == 1);
            }
            else // Neither pathology nor radiology
            {
                query = query.Where(x => x.IsPathology == 0 && x.IsRadiology == 0);
            }

            // Return the results
            return await query.ToListAsync();
        }
        //public virtual async Task UpdateDifferTraiff(ServiceDetail ObjServiceDetail, long OldTariffId ,long NewTariffId ,int UserId, string UserName )
        //{
        //    //throw new NotImplementedException();
        //    DatabaseHelper odal = new();
        //    string[] DetailEntity = { "ServiceDetailId", "ServiceId", "ClassId", "ClassRate", "Service", "TariffId" };
        //    var SEntity = ObjServiceDetail.ToDictionary();
        //    foreach (var rProperty in DetailEntity)
        //    {
        //        SEntity.Remove(rProperty);
        //    }
        //    // Add TraiffId manually to dictionary

        //    SEntity["OldTariffId"] = OldTariffId;
        //    SEntity["NewTariffId"] = NewTariffId;
        //    odal.ExecuteNonQuery("m_Assign_Servicesto_DifferTraiff", CommandType.StoredProcedure, SEntity);

        //}

        public virtual void UpdateDifferTariff(ServiceDetail ObjServiceDetail, long OldTariffId, long NewTariffId, int UserId, string UserName)
        {
            DatabaseHelper odal = new();

            string[] detailEntity = { "ServiceDetailId", "ServiceId", "ClassId", "ClassRate", "TariffId", "DiscountAmount", "DiscountPercentage", "Service" };
            var sEntity = ObjServiceDetail.ToDictionary();

            foreach (var rProperty in detailEntity)
            {
                sEntity.Remove(rProperty);
            }

            // Add parameters manually
            sEntity["OldTariffId"] = OldTariffId;
            sEntity["NewTariffId"] = NewTariffId;
            odal.ExecuteNonQuery("ps_Assign_Servicesto_DifferTraiff", CommandType.StoredProcedure, sEntity);
        }
        public virtual void InsertS(ServiceWiseCompanyCode ObjServiceWiseCompanyCode, int UserId, string UserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            DatabaseHelper odal = new();

            string[] SEntity = { "ServiceDetCompId", "ServiceId", "CompanyCode", "CompanyServicePrint", "IsInclusionOrExclusion", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var Entity = ObjServiceWiseCompanyCode.ToDictionary();

            foreach (var rProperty in SEntity)
            {
                Entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_insert_ServiceWiseCompanyCode", CommandType.StoredProcedure, Entity);
            scope.Complete();

        }
        public virtual async Task<List<ServiceMasterDTO>> GetServiceListwithTraiff(int TariffId, string ServiceName)
        {
            var qry = from s in _context.ServiceMasters
                      join d in _context.ServiceDetails on s.ServiceId equals d.ServiceId
                      where s.IsActive == true
                            && (ServiceName == "" || s.ServiceName.Contains(ServiceName))
                            && (TariffId == 0 || d.TariffId == TariffId)
                      select new ServiceMasterDTO()
                      {
                          ServiceId = s.ServiceId,
                          GroupId = s.GroupId,
                          ServiceShortDesc = s.ServiceShortDesc,
                          ServiceName = s.ServiceName,
                          ClassRate = d.ClassRate ?? 0,
                          TariffId = d.TariffId ?? 0,
                          ClassId = d.ClassId ?? 0,
                          IsEditable = s.IsEditable,
                          CreditedtoDoctor = s.CreditedtoDoctor,
                          IsPathology = s.IsPathology,
                          IsRadiology = s.IsRadiology,
                          IsActive = s.IsActive,
                          PrintOrder = s.PrintOrder,
                          IsPackage = s.IsPackage,
                          DoctorId = s.DoctorId,
                          IsDocEditable = s.IsDocEditable
                      };

            return await qry.Take(50).ToListAsync();

        }
        public virtual void Insert(List<MPackageDetail> ObjMPackageDetail, int UserId, string Username, long? PackageTotalDays, long? PackageIcudays, decimal? PackageMedicineAmount, decimal? PackageConsumableAmount)
        {
            DatabaseHelper odal = new();


            var tokensObj = new
            {
                ServiceId = Convert.ToInt32(ObjMPackageDetail[0].ServiceId)

            };
            odal.ExecuteNonQuery("Delete_PackageDetails", CommandType.StoredProcedure, tokensObj.ToDictionary());
            foreach (var item in ObjMPackageDetail)
            {

                string[] AEntity = { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in AEntity)
                {
                    Pentity.Remove(rProperty);
                }
                Pentity["PackageTotalDays"] = PackageTotalDays;

                Pentity["PackageIcudays"] = PackageIcudays;

                Pentity["PackageMedicineAmount"] = PackageMedicineAmount;
                Pentity["PackageConsumableAmount"] = PackageConsumableAmount;

                string VPackageId = odal.ExecuteNonQuery("PS_insert_PackageDetails", CommandType.StoredProcedure, "PackageId", Pentity);
                item.PackageId = Convert.ToInt32(VPackageId);

            }
        }
        public virtual BillingServiceNewDto GetServiceListNew(int TariffId)
        {
            BillingServiceNewDto objMain = new() { Data = new List<BillingServiceNew>(), Columns = new() };
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@TariffId", TariffId);
            DataTable dt = sql.FetchDataTableBySP("GET_SERVICES_NEW", para);
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName != "ServiceId" && dc.ColumnName != "ServiceName")
                {
                    objMain.Columns.Add(new BillingServiceColumns()
                    {
                        ClassId = dc.ColumnName.Split('|')[1].ToInt(),
                        ClassName = dc.ColumnName.Split('|')[0]
                    });
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                BillingServiceNew obj = new()
                {
                    ServiceId = dr["ServiceId"].ToInt(),
                    ServiceName = HIMS.Data.Extensions.DynamicLinqExpressionBuilder.ConvertToString(dr["ServiceName"]), // Explicitly specify the namespace
                    ColumnValues = new List<BillingServiceColumnValue>()
                };
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName != "ServiceId" && dc.ColumnName != "ServiceName")
                    {
                        obj.ColumnValues.Add(new BillingServiceColumnValue()
                        {
                            ClassId = dc.ColumnName.Split('|')[1].ToInt(),
                            ClassValue = dr[dc].ToInt()
                        });
                    }
                }
                objMain.Data.Add(obj);
            }
            return objMain;
        }
        public virtual async Task SaveServicesNew(int TariffId, List<BillingServiceNew> Data)
        {
            List<ServiceDetail> lst = new();
            foreach (var item in Data)
            {
                int serviceId = item.ServiceId;
                foreach (var cls in item.ColumnValues)
                {
                    lst.Add(new ServiceDetail() { ServiceId = serviceId, TariffId = TariffId, ClassId = cls.ClassId, ClassRate = cls.ClassValue });
                }
            }
            if (lst.Count > 0)
            {
                _context.ServiceDetails.RemoveRange(_context.ServiceDetails.Where(x => x.TariffId == TariffId));
                await _context.ServiceDetails.AddRangeAsync(lst);
                await _context.SaveChangesAsync();
            }
        }
    }
}




