using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IInPatientService
    {
        Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel objGrid);
        Task<IPagedList<InPatientSalesDetailsListDto>> Getsalesdetaillist(GridRequestModel objGrid);
        Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel objGrid);
        Task<IPagedList<SalesInPatientReturnDetailsListDto>> salesreturndetaillist(GridRequestModel objGrid);





    }
}
