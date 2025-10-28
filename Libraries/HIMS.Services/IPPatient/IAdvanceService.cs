using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IAdvanceService
    {
        Task<IPagedList<AdvanceListDto>> GetAdvanceListAsync(GridRequestModel objGrid);
        Task<IPagedList<RefundOfAdvanceListDto>> GetRefundOfAdvanceListAsync(GridRequestModel objGrid);
        Task InsertAdvanceAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objpayment, int CurrentUserId, string CurrentUserName);
        Task UpdateAdvanceSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment, int CurrentUserId, string CurrentUserName);

        Task<IPagedList<PatientWiseAdvanceListDto>> PatientWiseAdvanceList(GridRequestModel objGrid);

        Task<IPagedList<RefundOfAdvancesListDto>> GetAdvancesListAsync(GridRequestModel objGrid);
        void IPInsertSP(Refund Objrefund, AdvanceHeader ObjAdvanceHeader, List<AdvRefundDetail> ObjadvRefundDetailList, List<AdvanceDetail> ObjAdvanceDetailList, Payment ObjPayment, int UserId, string UserName);

        // Task InsertAsyncSP(Refund objRefund, AdvanceHeader objAdvanceHeader, AdvRefundDetail objAdvRefundDetail, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName);
        Task UpdateAdvance(AdvanceDetail OBJAdvanceDetail, int CurrentUserId, string CurrentUserName);
        void Cancel(AdvanceHeader ObjAdvanceHeader, long AdvanceDetailId);


    }
}
