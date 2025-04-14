using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface IMaterialConsumption
    {
        Task<IPagedList<MaterialConsumptionListDto>> MaterialConsumptionList(GridRequestModel objGrid);

        Task<IPagedList<MaterialConsumDetailListDto>> MaterialConsumptiondetailList(GridRequestModel objGrid);
        Task InsertAsync(TMaterialConsumptionHeader ObjTMaterialConsumptionHeader, int UserId, string Username);
        //Task UpdateAsync(TMaterialConsumptionHeader ObjTMaterialConsumptionHeader, int UserId, string Username);

        //Task InsertAsync1(TMaterialConsumptionDetail ObjTMaterialConsumptionDetail, int UserId, string Username);
        //Task UpdateAsync1(TMaterialConsumptionDetail ObjTMaterialConsumptionDetail, int UserId, string Username);

    }
}
