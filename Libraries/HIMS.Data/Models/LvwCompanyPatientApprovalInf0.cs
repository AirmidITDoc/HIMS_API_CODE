using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwCompanyPatientApprovalInf0
    {
        public string? PolicyNo { get; set; }
        public decimal EstimatedAmount { get; set; }
        public decimal HosApreAmt { get; set; }
        public decimal PathApreAmt { get; set; }
        public decimal RadiApreAmt { get; set; }
        public decimal FinalBillAmt { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal DisallowedAmt { get; set; }
        public decimal CompDiscount { get; set; }
        public decimal AdvBalAmt { get; set; }
        public string? ClaimNo { get; set; }
        public decimal HdiscAmt { get; set; }
        public decimal COutsideInvestAmt { get; set; }
        public decimal RecoveredByPatient { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public long AdmissionId { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal PharApreAmt { get; set; }
        public decimal MedicalApreAmt { get; set; }
    }
}
