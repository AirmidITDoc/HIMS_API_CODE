using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class BrowsePharmacyTPaymentReceiptListDto
    {

        public long? PaymentId { get; set; }
        public string? PatientName { get; set; }
        public string? SalesNo { get; set; }
        public string? PaymentDate { get; set; }
        public string? ReceiptNo { get; set; }

        public long? OP_IP_Type { get; set; }
        public string? PrintStoreName { get; set; }
        public long? SalesId { get; set; }
        //public DateTime? Date { get; set; }
        //public DateTime? Time { get; set; }

        public long TransactionType { get; set; }
        public long CashCounterId { get; set; }
        public long StoreId { get; set; }
        public long OPDIPDType { get; set; }

        public decimal? PayAmount { get; set; }
        public string? TranNo { get; set; }
        public string? BankName { get; set; }

        public DateTime? ValidationDate { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }

        public string? Comments { get; set; }
        public string? PayMode { get; set; }

        public string? OnlineTranNo { get; set; }
        public string? OnlineTranResponse { get; set; }

        public long? CompanyId { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public string? TransactionLabel { get; set; }

        public byte? IsSelfORCompany { get; set; }
        public string? TranMode { get; set; }

    }
}
