using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabBrowseRefundListDto
    {

     
        public long? RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public string? RefundNo { get; set; }

        public string? PBillNo { get; set; }
        //public long? OPD_IPD_Type { get; set; }
        //public long? OPD_IPD_ID { get; set; }

        public string? RegNo { get; set; }
        public DateTime? RegDate { get; set; }
        public string? PatientName { get; set; }
        public string? PatientType { get; set; }
        public string? MobileNo { get; set; }

        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }

        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public string? HospitalName { get; set; }

        public double? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public double? NetPayableAmt { get; set; }
        public double? BalanceAmt { get; set; }
        public double? RefundAmount { get; set; }


        public long? PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }

        public double? CashPayAmount { get; set; }
        public double? ChequePayAmount { get; set; }
        public double? CardPayAmount { get; set; }
        public double? AdvanceUsedAmount { get; set; }


        //public long? TransactionId { get; set; }
       // public long? TransactionType { get; set; }
       // public long? BillId { get; set; }

       // public string? PaymentType { get; set; }

        public string? Remark { get; set; }
        public string? PaymentRemark { get; set; }
        public string? AddedBy { get; set; }
    }
}
