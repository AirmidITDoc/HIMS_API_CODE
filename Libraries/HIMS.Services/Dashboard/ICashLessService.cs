using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DashBoard;
using HIMS.Data.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Dashboard
{
    public partial  interface ICashLessService
    {
        Task<IPagedList<CashlessPatientWiseSummaryDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<CashlessCountSummaryDto>> CashLessGetListAsync(GridRequestModel objGrid);
        Task<IPagedList<CashlessCompanyWiseSummaryDto>> CashLessCompanyWiseGetListAsync(GridRequestModel objGrid);




    }
}
