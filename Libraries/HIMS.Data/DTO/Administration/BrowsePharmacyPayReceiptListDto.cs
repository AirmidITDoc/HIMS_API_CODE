using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class BrowsePharmacyPayReceiptListDto
    {

        public long PaymentId { get; set; }
        public string PatientName { get; set; }
        public string SalesNo { get; set; }
        public string PaymentDate { get; set; }
        public string ReceiptNo { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        public decimal PaidAmount { get; set; }

        public long OP_IP_Type { get; set; }
        public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
        public string PrintStoreName { get; set; }
    }
}
