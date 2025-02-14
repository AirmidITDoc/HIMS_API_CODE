using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPSettlementService
    {
        Task InsertAsyncSP(Payment objpayment, int CurrentUserId, string CurrentUserName);
        Task InsertAsync(Payment objpayment, Bill objBill, int CurrentUserId, string CurrentUserName);

        Task UpdateAsync(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task UpdateAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<OPBillListSettlementListDto>> OPBillListSettlementList(GridRequestModel objGrid);


    }
}
