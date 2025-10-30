using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IOTBookingRequestService
    {
        Task<IPagedList<OTBookingRequestListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<OTBookingRequestEmergencyListDto>> GetListAsynco(GridRequestModel objGrid);
        void Cancel(TOtbookingRequest OBJOtbookingRequest, int UserId, string Username);
        Task InsertAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username);
        Task UpdateAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username, string[]? references);
        //Task<IPagedList<TOtRequestDiagnosis>> GetDiagnosisListAsync(GridRequestModel objGrid);
        Task<List<TOtRequestDiagnosisListDto>> GetDiagnosisListAsync(string descriptionType);



    }
}
