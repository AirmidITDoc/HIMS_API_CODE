using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
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
    public class MaterialConsumptionService : IMaterialConsumption
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MaterialConsumptionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<MaterialConsumptionListDto>> MaterialConsumptionList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MaterialConsumptionListDto>(model, "Rtrv_MaterialConsumption_ByName");
        }


        public virtual async Task InsertAsync(TMaterialConsumptionHeader ObjTMaterialConsumptionHeader, List<TMaterialConsumptionDetail> ObjTMaterialConsumptionDetail, int UserId, string Username)
        {
            //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "ConsumptionNo", "UpdatedBy", "AdmId" };
            var entity = ObjTMaterialConsumptionHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VMaterialConsumptionId = odal.ExecuteNonQuery("PS_insert_MaterialConsumption_1", CommandType.StoredProcedure, "MaterialConsumptionId", entity);
            ObjTMaterialConsumptionHeader.MaterialConsumptionId = Convert.ToInt32(VMaterialConsumptionId);



            foreach (var item in ObjTMaterialConsumptionDetail)
            {
                item.MaterialConsumptionId = Convert.ToInt32(VMaterialConsumptionId);

                string[] MEntity = { "MaterialConDetId", "AdmId", };
                var rentity = item.ToDictionary();
                foreach (var rProperty in MEntity)
                {
                    rentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("PS_insert_IMaterialConsumptionDetails", CommandType.StoredProcedure, rentity);

            }

        }


        //    public virtual async Task UpdateAsync(TMaterialConsumptionHeader ObjTMaterialConsumptionHeader, int UserId, string Username)
        //    {
        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {
        //            // Update header & detail table records
        //            _context.TMaterialConsumptionHeaders.Update(ObjTMaterialConsumptionHeader);
        //            _context.Entry(ObjTMaterialConsumptionHeader).State = EntityState.Modified;
        //            await _context.SaveChangesAsync();

        //            scope.Complete();
        //        }
        //    }


        //    public virtual async Task InsertAsync1(TMaterialConsumptionDetail ObjTMaterialConsumptionDetail, int UserId, string Username)
        //    {
        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {
        //            _context.TMaterialConsumptionDetails.Add(ObjTMaterialConsumptionDetail);
        //            await _context.SaveChangesAsync();

        //            scope.Complete();
        //        }
        //    }

        //    public virtual async Task UpdateAsync1(TMaterialConsumptionDetail ObjTMaterialConsumptionDetail, int UserId, string Username)
        //    {
        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {
        //            // Update header & detail table records
        //            _context.TMaterialConsumptionDetails.Update(ObjTMaterialConsumptionDetail);
        //            _context.Entry(ObjTMaterialConsumptionDetail).State = EntityState.Modified;
        //            await _context.SaveChangesAsync();

        //            scope.Complete();
        //        }
        //    }

        //}
    }
}
