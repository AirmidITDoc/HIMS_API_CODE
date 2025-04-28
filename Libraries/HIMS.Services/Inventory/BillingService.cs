using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public  class BillingService : IBillingService
    {
         private readonly Data.Models.HIMSDbContext _context;
            public BillingService(HIMSDbContext HIMSDbContext)
            {
                _context = HIMSDbContext;
            }
        public virtual async Task<IPagedList<BillingServiceDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BillingServiceDto>(model, "m_Rtrv_ServiceList_Pagn");
        }

        public virtual async Task<IPagedList<PackageServiceInfoListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PackageServiceInfoListDto>(model, "m_Rtrv_PackageServiceInfo");
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
        public virtual async Task UpdateAsync(ServiceMaster objService, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var lst = await _context.ServiceDetails.Where(x => x.ServiceId == objService.ServiceId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.ServiceDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
                // Update header & detail table records
                _context.ServiceMasters.Update(objService);
                _context.Entry(objService).State = EntityState.Modified;
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

        public virtual async Task UpdateDifferTariff(ServiceDetail ObjServiceDetail, long OldTariffId ,long NewTariffId ,int UserId, string UserName )
        {
            DatabaseHelper odal = new();

            string[] detailEntity = { "ServiceDetailId", "ServiceId", "ClassId", "ClassRate", "Service", "TariffId" };
            var sEntity = ObjServiceDetail.ToDictionary();

            foreach (var rProperty in detailEntity)
            {
                sEntity.Remove(rProperty);
            }

            // Add parameters manually
            sEntity["OldTariffId"] = OldTariffId;
            sEntity["NewTariffId"] = NewTariffId;
            odal.ExecuteNonQuery("m_Assign_Servicesto_DifferTraiff", CommandType.StoredProcedure, sEntity);
        }


    }
}

