using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IPathlogyService
    {
        Task<IPagedList<PathTestForUpdateListdto>> PathTestForUpdateList(GridRequestModel objGrid);
        Task<IPagedList<PathTemplateForUpdateListDto>> PathTemplateForUpdateList(GridRequestModel objGrid);
        Task<IPagedList<PathParaFillListDto>> PathParaFillList(GridRequestModel objGrid);
        Task<IPagedList<PathSubtestFillListDto>> PathSubtestFillList(GridRequestModel objGrid);
        Task<IPagedList<PathologyTestListDto>> PathologyTestList(GridRequestModel objGrid);
    }
}
