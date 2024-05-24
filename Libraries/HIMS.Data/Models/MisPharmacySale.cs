using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MisPharmacySale
    {
        public long PharmacySalesId { get; set; }
        public DateTime? ReportDate { get; set; }
        public long? ReportDay { get; set; }
        public long? ReportWeek { get; set; }
        public long? ReportMonth { get; set; }
        public long? ReportQuarter { get; set; }
        public long? ReportYear { get; set; }
        public long? StoreId { get; set; }
        public DateTime? SalesDate { get; set; }
        public decimal? SalesTotalAmount { get; set; }
        public decimal? SalesGstamount { get; set; }
        public decimal? SalesDiscAmount { get; set; }
        public decimal? SalesNetAmount { get; set; }
        public decimal? SalesPaidAmount { get; set; }
        public decimal? SalesBalanceAmount { get; set; }
        public decimal? SalesRoundOff { get; set; }
        public long? SalesCustomerCount { get; set; }
        public decimal? SalesAvgSales { get; set; }
        public decimal? SalesLandedAmount { get; set; }
        public decimal? SalesPurchaseAmount { get; set; }
        public decimal? SalesTotalMrpamount { get; set; }
        public decimal? PaymentCashPayAmount { get; set; }
        public decimal? PaymentChequePayAmount { get; set; }
        public decimal? PaymentCardPayAmount { get; set; }
        public decimal? PaymentNeftpayAmount { get; set; }
        public decimal? PaymentPayTmamount { get; set; }
        public decimal? SrtotalAmount { get; set; }
        public decimal? Srgstamount { get; set; }
        public decimal? SrdiscAmount { get; set; }
        public decimal? SrnetAmount { get; set; }
        public decimal? SrpaidAmount { get; set; }
        public decimal? SrbalanceAmount { get; set; }
        public decimal? SrpaymentCashPayAmount { get; set; }
        public decimal? SrpaymentChequePayAmount { get; set; }
        public decimal? SrpaymentCardPayAmount { get; set; }
        public decimal? SrpaymentNeftpayAmount { get; set; }
        public decimal? SrpaymentPayTmamount { get; set; }
        public DateTime? SlledgerDate { get; set; }
        public long? SlopeningBalanceQty { get; set; }
        public long? SlclosingBalanceQty { get; set; }
        public decimal? SlobbyPurchaseRate { get; set; }
        public decimal? SlcbbyPurchaseRate { get; set; }
        public decimal? SlobbyMrp { get; set; }
        public decimal? SlcbbyMrp { get; set; }
        public decimal? SlobbyLandedRate { get; set; }
        public decimal? SlcbbyLandedRate { get; set; }
        public DateTime? PurPurchaseDate { get; set; }
        public long? PurToStoreId { get; set; }
        public decimal? PurLandedTotalAmount { get; set; }
        public decimal? PurMrptotalAmount { get; set; }
        public decimal? PurPurTotalAmount { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
