using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;

namespace HIMS.Services.Nursing
{
    public partial interface ICanteenRequestService
    {

        Task InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username);
        Task<IPagedList<CanteenRequestListDto>> CanteenRequestsList(GridRequestModel objGrid);
        Task<IPagedList<CanteenRequestHeaderListDto>> CanteenRequestHeaderList(GridRequestModel objGrid);
        Task<IPagedList<DoctorNoteListDto>> DoctorNoteList(GridRequestModel objGrid);
        Task<IPagedList<TDoctorPatientHandoverListDto>> TDoctorPatientHandoverList(GridRequestModel objGrid);
        Task<List<CanteenListDto>> GetItemList(string ItemName);
        Task<List<CanteenListDto>> GetItemListForCanteen(string ItemName);
        Task CancelAsync(TCanteenRequestDetail ObjTCanteenRequestDetail, int CurrentUserId, string CurrentUserName);


    }
}
