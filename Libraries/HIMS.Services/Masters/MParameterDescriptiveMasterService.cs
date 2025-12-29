using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;

namespace HIMS.Services.Masters
{
    public partial class MParameterDescriptiveMasterService : IMParameterDescriptiveMasterService
    {
        //public virtual async Task<IPagedList<MParameterDescriptiveMasterListDto>> GetListAsync(GridRequestModel model)
        //{
        //    return await DatabaseHelper.GetGridDataBySp<MParameterDescriptiveMasterListDto>(model, "m_Rtrv_PathParameterDescriptiveMaster_by_Name");
        //}
        public virtual async Task<IPagedList<MParameterDescriptiveMasterListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MParameterDescriptiveMasterListDto>(model, "ps_Rtrv_PathDescriptiveValues_1");
        }

    }
}
