using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class IPBillListDto
    {
        public long BillNo { get; set; }
        public long? OpdIpdId { get; set; }
        public DateTime? BillDate { get; set; }
        public string? PbillNo { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? PatientAge { get; set; }
        public string? MobileNo { get; set; }
        public long? RegId { get; set; }
        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? PatientType { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public DateTime? BillTime { get; set; }
        public string? IPDNo { get; set; }
        public string? HospitalName { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? ConcessionAmt { get; set; }
        public decimal? CompDiscAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public long? OPD_IPD_ID { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public long? IsCancelled { get; set; }
        public byte? OpdIpdType { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public decimal? CashPay { get; set; }
        public decimal? ChequePay { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? AdvUsedPay { get; set; }
        public decimal? OnlinePay { get; set; }
        public decimal? PayCount { get; set; }
        public decimal? RefundAmount { get; set; }
        public decimal? RefundCount { get; set; }
        public string? CashCounterName { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? BillPrefix { get; set; }
        public string? BillMonth { get; set; }
        public string? BillYear { get; set; }
        public string? PrintBillNo { get; set; }
        public string? UserName { get; set; }
        public long? PatientTypeId { get; set; }












    }
}
