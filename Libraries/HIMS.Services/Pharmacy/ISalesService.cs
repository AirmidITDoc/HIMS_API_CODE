using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Master;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public partial interface ISalesService
    {
        Task InsertAsync(TSalesHeader user, Payment objPayment, int UserId, string Username);
        Task<IPagedList<PharSalesCurrentSumryListDto>> GetList(GridRequestModel objGrid);
        Task<IPagedList<PharCurrentDetListDto>> SalesDetailsList(GridRequestModel objGrid);
       
        Task<IPagedList<SalesDetailsListDto>> Getsalesdetaillist(GridRequestModel objGrid);

        Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel objGrid);


    }
}
