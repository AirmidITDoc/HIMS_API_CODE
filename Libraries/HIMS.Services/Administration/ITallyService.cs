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

        Task<IPagedList<TallyPhar2SalesDto>> TallyPhar2SalesListAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyPhar2PaymentDto>> TallyPhar2PaymentAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyPhar2SalesReturnDto>> TallyPhar2SalesReturnListAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyPhar2ReceiptDto>> TallyPhar2ReceiptListAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyIPBillListMediforteDto>> TallyIPBillListMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyIPBillDetailListMediforteDto>> TallyIPBillDetailListMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyOPBillListMediforteDto>> TallyOPBillListMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyOPBillDetailListMediforteDto>> TallyOPBillDetailListMediforteAsync(GridRequestModel objGrid);
        Task<IPagedList<TallyIPBillRefundListMediforteDto>> TallyIPBillRefundListMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyIPAdvancePaymentListMediforteDto>> TallyIPAdvancePaymentListMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyIPAdvanceRefundPaymentListMediforteDto>> TallyIPAdvanceRefundPaymentListMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyIPBillPaymentListMediforteDto>> TallyIPBillPaymentListMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyOPIPSalesDetailListMediforteDto>> TallyOPIPSalesDetailListMediforteAsync(GridRequestModel objGrid);
        Task<IPagedList<TallyPharmacyOPIPSalesReturnDetailListMediforteDto>> TallyPharmacyOPIPSalesReturnDetailListMediforteAsync(GridRequestModel objGrid);
        Task<IPagedList<TallyPharmacyOPIPSalesPaymentListMediforteDto>> TallyPharmacyOPIPSalesPaymentListMediforteAsync(GridRequestModel objGrid);


        Task<IPagedList<TallyOPPaymentMediforteDto>> TallyOPPaymentMediforteAsync(GridRequestModel objGrid);

        Task<IPagedList<TallyOPBillRefundPaymentMediforteDto>> TallyOPBillRefundPaymentMediforteAsync(GridRequestModel objGrid);










    }
}
