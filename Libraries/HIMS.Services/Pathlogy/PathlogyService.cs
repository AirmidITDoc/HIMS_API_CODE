using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public class PathlogyService : IPathlogyService
    {
        public virtual async Task<IPagedList<PathTemplateForUpdateListDto>> PathTemplateForUpdateList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathTemplateForUpdateListDto>(model, "Rtrv_PathTemplateForUpdate");
        }
        public virtual async Task<IPagedList<PathTestForUpdateListdto>> PathTestForUpdateList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathTestForUpdateListdto>(model, "Rtrv_PathTestForUpdate");
        }
        public virtual async Task<IPagedList<PathParaFillListDto>> PathParaFillList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathParaFillListDto>(model, "rtrv_PathParaFill");
        }
        public virtual async Task<IPagedList<PathSubtestFillListDto>> PathSubtestFillList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathSubtestFillListDto>(model, "rtrv_PathSubtestFill");
        }
        public virtual async Task<IPagedList<PathologyTestListDto>> PathologyTestList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathologyTestListDto>(model, "m_Rtrv_PathologyTestList");
        }
    }
}
