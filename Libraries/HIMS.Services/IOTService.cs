using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;

namespace HIMS.Services
{
    public partial interface IOTService
    {
        Task<IPagedList<OTBookinglistDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<requestAttendentListDto>> OTGetListAsync(GridRequestModel objGrid);

        //Task InsertAsync(TOtReservation OBJTOtbooking,List<TOtbookingRequest> requests,  int UserId, string Username);
        Task InsertAsync(TOtReservation OBJTOtbooking, int UserId, string Username);
        void InsertSP(TOtReservation ObjTOtReservation, int UserId, string Username);

        Task UpdateAsync(TOtReservation OBJTOtbooking, int UserId, string Username);
        void Cancel(TOtReservation objTOtReservation, int UserId, string Username);
        Task<TOtReservation?> GetByIdAsync(int id);
        List<OTRequestDetailsListSearchDto> SearchPatient(string Keyword);


    }
}
