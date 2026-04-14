using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IPaymentModeService
    {
        Task UpdateAsync(Payment objPayment, int UserId, string Username, string[]? references);
        Task<IPagedList<OPBillListForPaymentModeChangeListDto>> GetListAsync(GridRequestModel objGrid);
        Task PaymentUpdateAsync(List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<OPBillListForPaymentModeChangeListBillNoWiseDto>> GetBillListAsync(GridRequestModel objGrid);
        Task PaymentPharmacyUpdateAsync(List<TPaymentPharmacy> ObjTPaymentPharmacy, int CurrentUserId, string CurrentUserName);
        Task NewPaymentUpdateAsync(List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);
        Task Cancel(TPayment ObjTPayment, int UserId, string Username);
        Task NewUpdateAsync(PaymentPharmacy objPaymentPharmacy, int type, int UserId, string Username, string[]? references);

    }
}
