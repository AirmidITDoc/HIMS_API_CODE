using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class IPAdvanceListDto
    { 
        public long RegID { get; set; }
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string IPDNo { get; set; }
        public string CompanyName { get; set; }
        public string TariffName { get; set; }
        public string HospitalName { get; set; }
        public string MobileNo { get; set; }
        public string AgeYear { get; set; }
        public byte IsDischarged { get; set; }
        public byte IsBillGenerated { get; set; }
        public long AdvanceDetailID { get; set; }
        public DateTime Date { get; set; }
        public long AdvanceId { get; set; }
        public string AdvanceNo { get; set; }
        public long OPD_IPD_Id { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal UsedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public long AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public string Reason { get; set; }
        public long PaymentId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public DateTime ChequeDate { get; set; }
        public decimal CardPayAmount { get; set; }
        public string CardNo { get; set; }
        public string CardBankName { get; set; }
        public DateTime CardDate { get; set; }
        public string UserName { get; set; }
        public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
        public string DoctorName { get; set; }
        public string RefDoctorName { get; set; }
        public string WardName { get; set; }
        public string BedName { get; set; }
        public long TransactionType { get; set; }


    }
}
