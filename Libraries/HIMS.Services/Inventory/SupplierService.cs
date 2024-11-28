using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace HIMS.Services.Inventory
{
    public class SupplierService : ISupplierService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SupplierService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SupplierListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SupplierListDto>(model, "m_Rtrv_SupplierMasterList_by_Name");
        }
        public virtual async Task InsertAsyncSP(MSupplierMaster objSupplier, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { "TaxNature", "UpdatedBy", "PinCode","Taluka", "LicNo", "ExpDate", "DlNo", "BankId", "Bankname", "Branch", "BankNo", "Ifsccode",
                       "VenderTypeId", "OpeningBalance", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate","SupplierTime","MAssignSupplierToStores"};
                var entity = objSupplier.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vSupplierId = odal.ExecuteNonQuery("Insert_SupplierMaster_1_New", CommandType.StoredProcedure, "SupplierId", entity);
                objSupplier.SupplierId = Convert.ToInt32(vSupplierId);

                // Add details table records
                foreach (var objItem in objSupplier.MAssignSupplierToStores)
                {
                    objItem.SupplierId = objSupplier.SupplierId;
                }
                _context.MAssignSupplierToStores.AddRange(objSupplier.MAssignSupplierToStores);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Delete header table realted records
                MSupplierMaster? objSup = await _context.MSupplierMasters.FindAsync(objSupplier.SupplierId);
                if (objSup != null)
                {
                    _context.MSupplierMasters.Remove(objSup);
                }

                // Delete details table realted records
                var lst = await _context.MAssignSupplierToStores.Where(x => x.SupplierId == objSupplier.SupplierId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MAssignSupplierToStores.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }
        public virtual async Task InsertAsync(MSupplierMaster objSupplier, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MSupplierMasters.Add(objSupplier);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

         public virtual async Task UpdateAsync(MSupplierMaster objSupplier, int UserId, string Username)
         {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MSupplierMasters.Update(objSupplier);
                _context.Entry(objSupplier).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task CancelAsync(MSupplierMaster objSupplier, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                MSupplierMaster objsup = await _context.MSupplierMasters.FindAsync(objSupplier.SupplierId);
                _context.MSupplierMasters.Update(objsup);
                _context.Entry(objsup).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}



