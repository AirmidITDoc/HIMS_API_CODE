using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IOpeningBalanceService
    {
        Task<IPagedList<OpeningBalListDto>> GetOpeningBalanceList(GridRequestModel objGrid);

        Task<IPagedList<OpeningBalanaceItemDetailListDto>> GetOPningBalItemDetailList(GridRequestModel objGrid);

        Task OpeningBalSp(TOpeningTransactionHeader ObjTOpeningTransactionHeader, List<TOpeningTransactionDetail> ObjTOpeningTransaction, int CurrentUserId, string CurrentUserName);


    }
}
