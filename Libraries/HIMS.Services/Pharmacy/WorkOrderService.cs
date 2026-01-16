using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.Pharmacy
{
    public class WorkOrderService : IWorkOrderService
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
        public virtual async Task WorkOrderSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();

            string[] AEntity = { "Woid", "Date", "Time", "StoreId", "SupplierId", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "IsClosed", "Remark", "AddedBy", "IsCancelled", "IsCancelledBy" };

            var yentity = ObjTWorkOrderHeader.ToDictionary();
            foreach (var rProperty in yentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    yentity.Remove(rProperty);
            }
            string AWOId = odal.ExecuteNonQuery("insert_T_WorkOrderHeader_1", CommandType.StoredProcedure, "Woid", yentity);
            ObjTWorkOrderHeader.Woid = Convert.ToInt32(AWOId);

            foreach (var item in ObjTWorkOrderDetail)
            {
                item.Woid = Convert.ToInt32(AWOId);

                string[] rEntity = { "Woid", "ItemName", "Qty", "Rate", "TotalAmount", "DiscPer", "DiscAmount", "Vatper", "Vatamount", "NetAmount", "Remark" };
                var entity = item.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_T_WorkOrderDetail_1", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(TWorkOrderHeader), ObjTWorkOrderHeader.Woid.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            }
        }


        public virtual async Task UpdateSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Woid", "StoreId", "SupplierId", "TotalAmount", "DiscAmount", "VatAmount", "NetAmount", "Isclosed", "Remark", "UpdatedBy" };
            var Sentity = ObjTWorkOrderHeader.ToDictionary();
            foreach (var rProperty in Sentity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
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
                string[] DEntity = { "Woid", "ItemName", "Qty", "Rate", "TotalAmount", "DiscPer", "DiscAmount", "VatPer", "VatAmount", "NetAmount", "Remark" };
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_T_WorkOrderDetail_1", CommandType.StoredProcedure, pentity);
                await _context.LogProcedureExecution(pentity, nameof(TWorkOrderHeader), ObjTWorkOrderHeader.Woid.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);



            }
        }
    }
}

