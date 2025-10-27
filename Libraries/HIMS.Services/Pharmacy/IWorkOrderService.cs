using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IWorkOrderService
    {
        Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel objGrid);
        void WorkOrderSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int UserId, string Username);

        void UpdateSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int UserId, string Username);

        Task<IPagedList<WorkorderIteListDto>> GetOldworkeorderAsync(GridRequestModel objGrid);


    }
}
