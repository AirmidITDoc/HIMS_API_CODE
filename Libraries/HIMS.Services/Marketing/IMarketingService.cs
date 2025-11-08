using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Marketing;
using HIMS.Data.DTO.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Marketing
{
    public partial interface IMarketingService
    {
        Task<IPagedList<MarketingListDto>> MarketingAsync(GridRequestModel objGrid);

    }
}
