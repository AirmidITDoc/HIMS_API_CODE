using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Services.OTManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface ILabBrowseListService
    {
        Task<IPagedList<LabBrowsListDto>> GetLabListListAsync(GridRequestModel objGrid);
    }
}
