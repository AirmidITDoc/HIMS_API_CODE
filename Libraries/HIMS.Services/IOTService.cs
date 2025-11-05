using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;

namespace HIMS.Services
{
    public partial interface IOTService
    {
        Task<IPagedList<OTReservationListDto>> GetListOtReservationAsync(GridRequestModel objGrid);
        Task<IPagedList<requestAttendentListDto>> OTGetListAsync(GridRequestModel objGrid);
        Task InsertAsync(TOtReservation OBJTOtbooking, int UserId, string Username);
        void InsertSP(TOtReservation ObjTOtReservation, int UserId, string Username);
        Task UpdateAsync(TOtReservation OBJTOtbooking, int UserId, string Username);
        void Cancel(TOtReservation objTOtReservation, int UserId, string Username);
        Task<TOtReservation?> GetByIdAsync(int id);
        List<OTRequestDetailsListSearchDto> SearchPatient(string Keyword);
        Task InsertAsync(TOtReservationHeader ObjTOtReservationHeader, int UserId, string Username);
        Task UpdateAsync(TOtReservationHeader ObjTOtReservationHeader, int UserId, string Username, string[]? references);


    }
}
