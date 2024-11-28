using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
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
        public virtual async Task InsertAsyncSP(ServiceMaster objService, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { " SubGroupId  ", "DoctorId", "IsEmergency ", "EmgAmt", " EmgPer", " IsDocEditable", "  CreatedBy ", " CreatedDate", "AddedBy ", "ServiceDetail" };
                var entity = objService.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string VServiceId = odal.ExecuteNonQuery("m_insert_ServiceMaster_1", CommandType.StoredProcedure, "ServiceId", entity);
                objService.ServiceId = Convert.ToInt32(VServiceId);



                // Add details table records
                foreach (var objTemplate in objService.ServiceDetails)
                {
                    objTemplate.ServiceId = objService.ServiceId;
                }
                _context.ServiceDetails.AddRange(objService.ServiceDetails);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Delete header table realted records
                ServiceMaster? objBill = await _context.ServiceMasters.FindAsync(objService.ServiceId);
                if (objBill != null)
                {
                    _context.ServiceMasters.Remove(objBill);
                }

                // Delete details table realted records
                var lst = await _context.ServiceDetails.Where(x => x.ServiceId == objService.ServiceId).ToListAsync(); 
                if (lst.Count > 0)
                {
                    _context.ServiceDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
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
    }
 }

