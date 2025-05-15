using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface ISalesReturnService
    {

        Task<IPagedList<SalesReturnDetailsListDto>> salesreturndetaillist(GridRequestModel objGrid);

        Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel objGrid);
        Task<IPagedList<SalesRetrunCurrentSumryListDto>> SalesReturnSummaryList(GridRequestModel objGrid);
        Task<IPagedList<SalesRetrunLCurrentDetListDto>> SalesReturnDetailsList(GridRequestModel objGrid);
        Task InsertAsyncSP(TSalesReturnHeader ObjTSalesReturnHeader, /*TCurrentStock  ObjTCurrentStock*/ int UserId, string Username);


    }
}
