using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;

namespace HIMS.Services.Masters
{
    public partial interface IMParameterDescriptiveMasterService
    {
        //   Task<IPagedList<MParameterDescriptiveMasterListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<MParameterDescriptiveMasterListDto>> GetListAsync1(GridRequestModel objGrid);


    }
}
