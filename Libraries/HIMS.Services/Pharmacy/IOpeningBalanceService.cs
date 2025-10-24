using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IOpeningBalanceService
    {
        Task<IPagedList<OpeningBalListDto>> GetOpeningBalanceList(GridRequestModel objGrid);

        Task<IPagedList<OpeningBalanaceItemDetailListDto>> GetOPningBalItemDetailList(GridRequestModel objGrid);

        void OpeningBalSp(TOpeningTransactionHeader ObjTOpeningTransactionHeader, List<TOpeningTransactionDetail> ObjTOpeningTransaction, int UserId, string Username);


    }
}
