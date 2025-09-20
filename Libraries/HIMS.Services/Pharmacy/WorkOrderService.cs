using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Pharmacy
{
    public  class WorkOrderService : IWorkOrderService
    {
      
        private readonly Data.Models.HIMSDbContext _context;
        public WorkOrderService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WorkOrderListDto>(model, "Rtrv_WorkOrderList_by_Name");
        }
        public virtual async Task<IPagedList<WorkorderIteListDto>> GetOldworkeorderAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WorkorderIteListDto>(model, "Rtrv_ItemDetailsForWorkOrderUpdate");
        }
        public virtual async Task WorkOrderAsyncSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int UserId, string UserName)
        {
            DatabaseHelper odal = new();

            string[] AEntity = { "Woid", "Wono", "IsCancelDate", "UpdatedBy" };

            var yentity = ObjTWorkOrderHeader.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                yentity.Remove(rProperty);
            }
            string AWOId = odal.ExecuteNonQuery("insert_T_WorkOrderHeader_1", CommandType.StoredProcedure, "WOId", yentity);
            //ObjTWorkOrderHeader.Woid = Convert.ToInt32(AWOId);

            foreach (var item in ObjTWorkOrderDetail)
            {
                item.Woid = Convert.ToInt32(AWOId);

                string[] rEntity = { "WodetId", "PendQty" };
                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_T_WorkOrderDetail_1", CommandType.StoredProcedure, entity);
            }
        }


        public virtual async Task UpdateAsyncSp(TWorkOrderHeader ObjTWorkOrderHeader,List<TWorkOrderDetail> ObjTWorkOrderDetail, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Wono", "Date", "Time", "IsCancelled", "IsCancelledBy", "IsCancelDate", "AddedBy" };
            var Sentity = ObjTWorkOrderHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Sentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("Update_WorkorderHeader", CommandType.StoredProcedure, Sentity);

            var tokensObj = new
            {
                WOId = Convert.ToInt32(ObjTWorkOrderHeader.Woid)
            };
            odal.ExecuteNonQuery("Delete_WODetails_1", CommandType.StoredProcedure, tokensObj.ToDictionary());

            foreach (var item in ObjTWorkOrderDetail)

            {
                string[] DEntity = { "WodetId", "PendQty" };
                var pentity = item.ToDictionary();
                foreach (var Property in DEntity)
                {
                    pentity.Remove(Property);
                }
                odal.ExecuteNonQuery("insert_T_WorkOrderDetail_1", CommandType.StoredProcedure, pentity);


            }
        }

       
    }
}

