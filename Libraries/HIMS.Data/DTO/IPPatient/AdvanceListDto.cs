using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class AdvanceListDto
    {
        public long RegID { get; set; }
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string IPDNo { get; set; }
        public string CompanyName { get; set; }
        public string TariffName { get; set; }
        public string HospitalName { get; set; }
        public string MobileNo { get; set; }
        public string? AgeYear { get; set; }
        public string IsDischarged { get; set; }
        public string IsBillGenerated { get; set; }
        public long AdvanceDetailID { get; set; }
        public DateTime Date { get; set; }
        public long AdvanceId { get; set; }
        public string AdvanceNo { get; set; }
        public long OPD_IPD_Id { get; set; }
        public byte? OpdIpdType { get; set; }
        public string AdvanceAmount { get; set; }
        public string UsedAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string RefundAmount { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public string? Reason { get; set; }
        public long PaymentId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string DoctorName { get; set; }
        public string RefDoctorName { get; set; }
        public string WardName { get; set; }
        public string BedName { get; set; }
        public string CardPayAmount { get; set; }
        public string PayTMAmount { get; set; }
        public string UserName { get; set; }
        public long? TransactionType { get; set; }

    }

}
