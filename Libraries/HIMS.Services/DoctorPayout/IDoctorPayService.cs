using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

namespace HIMS.Services.DoctorPayout
{
    public partial interface IDoctorPayService
    {
        Task InsertAsync(TAdditionalDocPay ObjTAdditionalDocPay, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<DoctorPayListDto>> GetList(GridRequestModel objGrid);
        Task<IPagedList<DoctorBilldetailListDto>> GetBillDetailList(GridRequestModel objGrid);
        Task<IPagedList<DoctorPaysummarydetailListDto>> GetDoctorsummaryDetailList(GridRequestModel objGrid);
        Task<IPagedList<DcotorpaysummaryListDto>> GetDoctroSummaryList(GridRequestModel objGrid);
        Task UpdateAsync(List<AddCharge> ObjAddCharge, int CurrentUserId, string CurrentUserName);
        //Task InsertAsync(TDoctorPayoutProcessHeader ObjTDoctorPayoutProcessHeader, int UserId, string Username);
        //Task UpdateAsync(TDoctorPayoutProcessHeader ObjTDoctorPayoutProcessHeader, int UserId, string Username, string[]? references);
        Task<IPagedList<DoctorShareListDto>> GetLists(GridRequestModel objGrid);
        Task<IPagedList<DoctorShareLbyNameListDto>> GetList1(GridRequestModel objGrid);
        Task InsertAsync(AddCharge ObjAddCharge, int UserId, string Username);
        void InsertSP(TDoctorPayoutProcessHeader ObjTDoctorPayoutProcessHeader, List<TDoctorPayoutProcessDetail> ObjTDoctorPayoutProcessDetail,  int UserId, string Username);



    }
}
