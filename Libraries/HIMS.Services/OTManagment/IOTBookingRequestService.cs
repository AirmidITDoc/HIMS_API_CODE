using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IOTBookingRequestService
    {
        Task<IPagedList<OTBookingRequestListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<OTBookingRequestEmergencyListDto>> GetListAsynco(GridRequestModel objGrid);
        Task<IPagedList<OtRequestSurgeryDetailListDto>> GetListAsyncs(GridRequestModel objGrid);
        Task<IPagedList<OtRequestListDto>> GetListAsyncot(GridRequestModel objGrid);
        Task<IPagedList<OtRequestAttendingDetailListDto>> GetListAsyncor(GridRequestModel objGrid);
        void Cancel(TOtbookingRequest OBJOtbookingRequest, int UserId, string Username);
        Task InsertAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username);
        Task UpdateAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username, string[]? references);
        Task<List<TOtRequestDiagnosisListDto>> GetDiagnosisListAsync(string descriptionType);

    }
}
