using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class FinancialDashboard
    {
        public List<BedOccupancyCountSummary> BedOccupancyCountSummary { get; set; }
        public List<ServiceCharges> ServiceCharges { get; set; }
        public List<ReceiptPayment> ReceiptPayment { get; set; }
        public List<BillSummary> BillSummary { get; set; }
        public List<ReceiptOPIP> ReceiptOPIP { get; set; }
        public List<AdvanceOPIP> AdvanceOPIP { get; set; }
        public List<RefundOPIP> RefundOPIP { get; set; }
        public List<PharmacyReturn> PharmacyReturn { get; set; }
        public List<TypeOfVisit> TypeOfVisit { get; set; }
        public List<DoctorWisePatientCount> DoctorWisePatientCount { get; set; }
        public List<FinancialOPExistingPatientCount> FinancialOPExistingPatientCount { get; set; }
        public List<FinancialIPExistingPatientCount> FinancialIPExistingPatientCount { get; set; }
        public List<IPRefDoctorCount> IPRefDoctorCount { get; set; }
        //public List<PharmacyOPDPatientSale> PharmacyOPDPatientSale { get; set; }
        public List<PharmacySaleOP> PharmacySaleOP { get; set; }
        public List<PharmacySaleIP> PharmacySaleIP { get; set; }
        public List<FinancialAdvanceBalance> FinancialAdvanceBalance { get; set; }
        public List<FinancialOutStandingOPIP> FinancialOutStandingOPIP { get; set; }
        public List<OutandingwithdateCaverage> OutandingwithdateCaverage { get; set; }
        public List<InsuranceCaverageAdequacy> InsuranceCaverageAdequacy { get; set; }

    }
    public class BedOccupancyCountSummary
    {
        public string? RoomName { get; set; }
        public long TotalBeds { get; set; }
        public long OccupiedBeds { get; set; }
        public decimal? OccupancyPercent { get; set; }


    }
    public class ServiceCharges
    {
        public string? ServiceName { get; set; }
        public double OPTotalAMT { get; set; }
        public decimal OPDiscount { get; set; }
        public decimal OPCollection { get; set; }
        public double IPTotalAMT { get; set; }
        public decimal IPDiscount { get; set; }
        public decimal IPCollection { get; set; }

    }
    public class ReceiptPayment
    {
        public string? ServiceName { get; set; }
        public decimal OPCollection { get; set; }
        public decimal IPCollection { get; set; }
    }
    public class BillSummary
    {
        public decimal? Cash { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? NEFT { get; set; }
        public decimal? Cheque { get; set; }
        public decimal? UPI { get; set; }
        public decimal? UsedAdvance { get; set; }



    }
    public class ReceiptOPIP
    {
        public decimal Receipt { get; set; }


    }
    public class AdvanceOPIP
    {
        public decimal Advance { get; set; }
    }
    public class RefundOPIP
    {
        public decimal Refund { get; set; }


    }
    public class PharmacyReturn
    {
        public decimal? Return1 { get; set; }
    }
    public class TypeOfVisit
    {
        public string? TypeOFVisit { get; set; }
        public long PatientCount { get; set; }

    }
    public class DoctorWisePatientCount
    {
        public string? DoctorName { get; set; }
        public long PatientCount { get; set; }
        public decimal? OPCollection { get; set; }

    }
    public class FinancialOPExistingPatientCount
    {
        public long OPNewPatientCount { get; set; }
        public long OPExistingPatientCount { get; set; }
    }
    public class FinancialIPExistingPatientCount
    {
        public long IPNewPatientCount { get; set; }
        public long IPExistingPatientCount { get; set; }
    }

    public class IPRefDoctorCount
    {
        public string? RefName { get; set; }
        public long OPRefCount { get; set; }
        public long ipRefCount { get; set; }

    }



    public class PharmacySaleOP
    {
        public decimal OPTotalLandedAmount { get; set; }
        public decimal OPNetAmount { get; set; }
        public decimal OPprofitamount { get; set; }

    }
    public class PharmacySaleIP
    {
        public decimal IPTotalLandedAmount { get; set; }
        public decimal IPNetAmount { get; set; }
        public decimal IPprofitamount { get; set; }
    }
    public class FinancialAdvanceBalance
    {
        public double UnadjestAdvance { get; set; }

    }


    public class FinancialOutStandingOPIP
    {
        public decimal OPOustandingAMT { get; set; }
        public decimal IPOutstandingAMT { get; set; }
        public decimal TotalOutstanding { get; set; }
    }
    public class OutandingwithdateCaverage
        {
        public decimal OPOustandingAMTDate { get; set; }
        public decimal IPOutstandingAMTDate { get; set; }
        public decimal TotalOutstandingdate { get; set; }
    }
    public class InsuranceCaverageAdequacy
    {
        public decimal IPApprovedAmount { get; set; }
        public decimal IPUnpaidCharges { get; set; }
        public long UnadjestedAdvance { get; set; }
        public long InsuranceAdequancy { get; set; }
    }
}

