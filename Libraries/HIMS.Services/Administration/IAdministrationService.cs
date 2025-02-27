using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IAdministrationService
    {
        Task<IPagedList<PaymentModeDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<RoleMasterListDto>> RoleMasterList(GridRequestModel objGrid);
        
        
        Task<IPagedList<BrowseOPDBillPagiListDto>> BrowseOPDBillPagiList(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPAdvPayPharReceiptListDto>> BrowseIPAdvPayPharReceiptList(GridRequestModel objGrid);


    }
}
