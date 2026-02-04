using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface ILabApprovalService
    {
        Task<IPagedList<LabResultCompletedListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
