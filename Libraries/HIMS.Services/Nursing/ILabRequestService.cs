using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Nursing
{
    public partial interface ILabRequestService
    {
        Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username);
        Task<IPagedList<LabRequestListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<LabRequestDetailsListDto>> SPGetListAsync(GridRequestModel objGrid);
        Task CancelAsync(THlabRequest objTHlabRequest, int UserId, string Username);
        //Task<PatientAdmittedListSearchDto> PatientByAdmissionId(long admissionId);


    }
}
