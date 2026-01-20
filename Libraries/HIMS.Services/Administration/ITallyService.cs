using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial  interface ITallyService
    {

        Task<IPagedList<TallyListDto>> OPBillCashCounterListAsync(GridRequestModel objGrid);

        Task<IPagedList<OPRefundBillListCashCounterDto>> OPRefundBillListAsync(GridRequestModel objGrid);

        Task<IPagedList<IPAdvRefundPatientWisePaymentDto>> IPAdvRefundPatientWisePaymentlistAsync(GridRequestModel objGrid);

        Task<IPagedList<IPBillListPatientWisePaymentDto>> IPBillListPatientWisePaymentListAsync(GridRequestModel objGrid);

        Task<IPagedList<IPAdvPatientWisePaymentDto>> IPAdvPatientWisePaymentListAsync(GridRequestModel objGrid);

        Task<IPagedList<IPBillListPatientWiseDto>> IPBillListPatientWiseListAsync(GridRequestModel objGrid);

        Task<IPagedList<IPBillListCashCounterDto>> IPBillCashCounterListAsync(GridRequestModel objGrid);

        Task<IPagedList<IPBillRefundBillListPatientWisePaymentDto>> IPBillRefundBillPatientWisePaymentListAsync(GridRequestModel objGrid);

        Task<IPagedList<PurchaseWiseSupplierDto>> PurchaseWiseSupplierListAsync(GridRequestModel objGrid);

    }
}
