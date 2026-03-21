using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Canteen
{
    public partial interface ICanteenBillService
    {
        Task InsertAsync(TCanteenBillHeader ObjTCanteenBillHeader, int UserId, string Username);
        Task UpdateAsync(TCanteenBillHeader ObjTCanteenBillHeader, int UserId, string Username, string[]? references);


        Task<IPagedList<CanteenBillListDo>> CanteenBillList(GridRequestModel model);

        Task<IPagedList<CanteenBillDetailsLisDto>> CanteenBilldetailList(GridRequestModel model);


    }
}
