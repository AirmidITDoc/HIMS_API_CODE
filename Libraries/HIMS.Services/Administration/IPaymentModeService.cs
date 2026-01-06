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


    }
}
