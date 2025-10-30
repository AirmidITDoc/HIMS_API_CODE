using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IOTBookingRequestService
    {
        //Task InsertAsync(TOtbookingRequest objOTBooking, int UserId, string Username);
        //Task UpdateAsync(TOtbookingRequest objOTBooking, int UserId, string Username);
        Task<IPagedList<OTBookingRequestListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<OTBookingRequestEmergencyListDto>> GetListAsynco(GridRequestModel objGrid);

        void Cancel(TOtbookingRequest OBJOtbookingRequest, int UserId, string Username);

    }
}
