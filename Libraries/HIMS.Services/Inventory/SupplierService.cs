using HIMS.Data;
using HIMS.Data.DataProviders;
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
        //      public virtual async Task InsertAsyncSP(MSupplierMaster objSupplier, int UserId, string Username)

        //{
        //          try
        //          {
        //              //Add header table records
        //              DatabaseHelper odal = new();
        //              string[] rEntity = { "SupplierId", "UpdatedBy", "PinCode","TaxNature", "Taluka", "LicNo", "ExpDate", "DlNo", "BankId", "Bankname", "Branch", "BankNo", "Ifsccode",
        //               "VenderTypeId", "OpeningBalance", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate","SupplierTime","MAssignSupplierToStores"};
        //              var entity = objSupplier.ToDictionary();
        //              foreach (var rProperty in rEntity)
        //              {
        //                  entity.Remove(rProperty);
        //              }
        //              string vSupplierId = odal.ExecuteNonQuery("Insert_SupplierMaster_1_New", CommandType.StoredProcedure, "SupplierId", entity);
        //              objSupplier.SupplierId = Convert.ToInt32(vSupplierId);

        //             // Add details table records
        //              foreach (var objItem in objSupplier.MAssignSupplierToStores)
        //              {
        //                  objItem.SupplierId = objSupplier.SupplierId;
        //              }
        //              _context.MAssignSupplierToStores.AddRange(objSupplier.MAssignSupplierToStores);
        //              //await _context.SaveChangesAsync(UserId, Username);
        //          }
        //          catch (Exception)
        //          {
        //              // Delete header table realted records
        //              MSupplierMaster? objSup = await _context.MSupplierMasters.FindAsync(objSupplier.SupplierId);
        //              if (objSup != null)
        //              {
        //                  _context.MSupplierMasters.Remove(objSup);
        //              }

        //              // Delete details table realted records
        //              var lst = await _context.MAssignSupplierToStores.Where(x => x.SupplierId == objSupplier.SupplierId).ToListAsync();
        //              if (lst.Count > 0)
        //              {
        //                  _context.MAssignSupplierToStores.RemoveRange(lst);
        //              }
        //              await _context.SaveChangesAsync();
        //          }
        //      }




        // 04/09/2024
        public virtual async Task InsertAsync(MSupplierMaster objSupplier, List<MAssignSupplierToStore> newSupplierStore, int UserId, string Username)
        {

            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                objSupplier.SupplierId = (objSupplier != null) ? objSupplier.SupplierId : long.Parse("0");
                 _context.MSupplierMasters.Add(objSupplier);
                await _context.SaveChangesAsync();

                var supplierId = objSupplier.SupplierId;

                foreach (var store in objSupplier.MAssignSupplierToStores)
                {
                    var assignSupplierToStore = new MAssignSupplierToStore
                    {
                        StoreId = store.StoreId,
                        AssignId = store.AssignId,
                        SupplierId = supplierId
                    };
                    _context.MAssignSupplierToStores.Add(assignSupplierToStore);
                }

                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }



        // I created//
        public virtual async Task InsertAsync(MSupplierMaster objSupplier, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Insert into MSupplierMaster table
                objSupplier.SupplierId = (objSupplier != null) ? objSupplier.SupplierId : long.Parse("0");
                _context.MSupplierMasters.Add(objSupplier);
                await _context.SaveChangesAsync();

                // Insert into MAssignSupplierToStore table
                var supplierStore = new MAssignSupplierToStore
                {
                    SupplierId = objSupplier.SupplierId,
                    AssignId = UserId,
                    StoreId = UserId,

                };

                _context.MAssignSupplierToStores.Add(supplierStore);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
         public virtual async Task UpdateAsync(MSupplierMaster objSupplier, int UserId, string Username)
         {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var supplierStore = await _context.MAssignSupplierToStores.Where(x => x.SupplierId == objSupplier.SupplierId).ToListAsync();
                _context.MAssignSupplierToStores.RemoveRange(supplierStore);

                // Update header & detail table records
                _context.MSupplierMasters.Update(objSupplier);
                _context.Entry(objSupplier).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}



