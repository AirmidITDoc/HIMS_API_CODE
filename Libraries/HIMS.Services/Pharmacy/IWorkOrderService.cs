using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial  interface IWorkOrderService
    {
        Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel objGrid);
        Task WorkOrderAsyncSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail,  int UserId, string Username);
        //Task InsertAsync(TWorkOrderHeader ObjTWorkOrderHeader,int UserId, string Username);
        //Task UpdateAsync(TWorkOrderHeader ObjTWorkOrderHeader, int UserId, string Username);
        Task UpdateAsyncSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int UserId, string Username);



    }
}
