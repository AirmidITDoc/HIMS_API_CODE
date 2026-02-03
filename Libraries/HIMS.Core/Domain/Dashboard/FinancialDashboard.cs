using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class FinancialDashboard
    {
        public PatientcountWardWiseCountSummary CountSummary { get; set; }
        public List<FinancialCount> FinancialCount { get; set; }
        public List<FinancialTestCount>FinancialTestCount { get; set; }
        public List<FinancialOPPayment> FinancialOPPayment { get; set; }
        public List<FinancialIPPayment> FinancialIPPayment { get; set; }
        public List<FinancialVisit> FinancialVisit { get; set; }
        public List<FinancialCollectionPayMode> FinancialCollectionPayMode { get; set; }
        public List<FinancialAdvance> FinancialAdvance { get; set; }
        public List<FinancialPharmacyReturn> FinancialPharmacyReturn { get; set; }
        public List<FinancialRefund> FinancialRefund { get; set; }
        public List<FinancialDoctorWisePatientCountSummary> FinancialDoctorWisePatientCountSummary { get; set; }
        public List<FinancialModeWiseCollection> FinancialModeWiseCollection { get; set; }
        public List<FinancialOPExistingPatientCount> FinancialOPExistingPatientCount { get; set; }
        public List<FinancialIPExistingPatientCount> FinancialIPExistingPatientCount { get; set; }
        public List<FinancialOPDPatientSale> FinancialOPDPatientSale { get; set; }
        public List<FinancialIPDPatientSale> FinancialIPDPatientSale { get; set; }
        public List<FinancialAdvanceBalance> FinancialAdvanceBalance { get; set; }
        public List<FinancialOutStandingOPIP> FinancialOutStandingOPIP { get; set; }
        public List<FinancialInsuranceCaverageAdequacy> FinancialInsuranceCaverageAdequacy { get; set; }

    }
    public class PatientcountWardWiseCountSummary
    {
        public string? WardName { get; set; }
        public long PatientCountt { get; set; }
        public long PatientOccupancy { get; set; }
    }
    public class FinancialCount
    {
        public string GroupName { get; set; }
        public decimal Discount { get; set; }
        public decimal OPCollection { get; set; }

    }
    public class FinancialTestCount
    {
        public string GroupName { get; set; }
        public decimal Discount { get; set; }
        public decimal IPCollection { get; set; }
    }
    public class FinancialOPPayment
    {
        public string GroupName { get; set; }
        public decimal Discount { get; set; }
        public decimal OPCollection { get; set; }


    }
    public class FinancialIPPayment
    {
        public string GroupName { get; set; }
        public decimal Discount { get; set; }
        public decimal IPCollection { get; set; }


    }
    public class FinancialVisit 
    {
        public string? TypeOFVisit { get; set; }
        public long PatientCount { get; set; }
    }
    public class FinancialCollectionPayMode
    {
        public decimal? CashPay { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? PayTM { get; set; }
        public decimal? Cheque { get; set; }

    }
    public class FinancialAdvance
    {
        public double? Advance { get; set; }
    }
    public class FinancialPharmacyReturn
    {
        public decimal? PharmacyReturn { get; set; }
    }
    public class FinancialRefund
    {
        public decimal? RefundAMT { get; set; }
    }
    public class FinancialDoctorWisePatientCountSummary
    {
        public string? DoctorName { get; set; }
        public decimal? PatientCount { get; set; }
        public decimal? OPCollection { get; set; }

    }
    public class FinancialModeWiseCollection
    {
        public decimal? Cash { get; set; }
        public decimal? Cardpayment { get; set; }
        public decimal? PayTMPayment { get; set; }
        public decimal? ChequePayment { get; set; }
        public decimal? NEFTPayment { get; set; }
    }
    public class FinancialOPExistingPatientCount
    {
        public long OPNewPatientCount { get; set; }
        public long OPExistingPatientCount { get; set; }
    }
    public class FinancialIPExistingPatientCount
    {
        public long? IPNewPatientCount { get; set; }
        public long? IPExistingPatientCount { get; set; }
    }
    public class FinancialOPRefDoctorPatientCount

    {
        public decimal? OPRefDoctorPatientCount { get; set; }
    }
    public class FinancialIPRefDoctorPatientCount
    {
        public decimal? IPRefDoctorPatientCount { get; set; }
    }
    public class FinancialOPDPatientSale
    {
        public decimal? TotalAmt { get; set; }
        public decimal? NetAMT { get; set; }
        public decimal? PaidAMT { get; set; }
        public decimal? BalAMT { get; set; }
    }
    public class FinancialIPDPatientSale
    {
        public decimal? TotalAmt { get; set; }
        public decimal? NetAMT { get; set; }
        public decimal? PaidAMT { get; set; }
        public decimal? BalAMT { get; set; }
    }
    public class FinancialAdvanceBalance
    {
        public decimal? UnadjestAdvance { get; set; }
       
    }
    public class FinancialOutStandingOPIP
    {
        public decimal? OPOustandingAMT { get; set; }
        public decimal? IPOutstandingAMT { get; set; }
        public decimal? TotalOutstanding { get; set; }
    }
    public class FinancialInsuranceCaverageAdequacy
    {
        public decimal? IP_ApprovedAmount { get; set; }
        public decimal? IP_UnpaidCharges { get; set; }
        public decimal? Unadjested_Advance { get; set; }
        public decimal? Insurance_Adequancy { get; set; }


    }
}
