using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IWorkOrderService
    {
        Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel objGrid);
        Task WorkOrderSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int CurrentUserId, string CurrentUserName);

        Task UpdateSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int CurrentUserId, string CurrentUserName);

        Task<IPagedList<WorkorderIteListDto>> GetOldworkeorderAsync(GridRequestModel objGrid);


    }
}
