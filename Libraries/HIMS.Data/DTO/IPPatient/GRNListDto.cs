using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class GRNListDto
    {
        public long SupplierId { get; set; }
        public long Grnid { get; set; }
        public string? SupplierName { get; set; }
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? GrnNumber { get; set; }
        public string Grndate { get; set; }
        public string Grntime { get; set; }
        public string? InvoiceNo { get; set; }
        public string? DeliveryNo { get; set; }
        public string? GateEntryNo { get; set; }
        public bool? Cash_CreditType { get; set; }
        public bool? Grntype { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVatamount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? LandedRate { get; set; }
        public string? Remark { get; set; }
        public string? ReceivedBy { get; set; }
        public bool? IsVerified { get; set; }
        public long? IsClosed { get; set; }
        public long? AddedBy { get; set; }
        public string InvDate { get; set; }
        public decimal? DebitNote { get; set; }
        public decimal? CreditNote { get; set; }
        public decimal? OtherCharge { get; set; }
        public decimal? RoundingAmt { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? TotSGSTAmt { get; set; }
        public decimal? TotIGSTAmt { get; set; }
        public decimal? BillDiscAmt { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? EditSupplier { get; set; }
        public string? EwayBillNo { get; set; }
        public string? EwayBillDate { get; set; }

    }

    public class BatchListDTO
    {
        public string? FormattedText { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? UnitPurRate { get; set; }
        public decimal? UnitLandedRate { get; set; }
        public decimal? GST { get; set; }
    }
}