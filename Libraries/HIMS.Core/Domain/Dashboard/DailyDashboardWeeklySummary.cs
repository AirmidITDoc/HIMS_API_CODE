using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class DailyDashboardWeeklySummary
    {
        public DailyPatientStatus PatientStatus { get; set; }
        public DailyPaymentSummary PaymentSummary { get; set; }

        public List<WeeklyOPTrend> WeeklyOPTrend { get; set; }
        public List<WeeklyAdmissionDischarge> WeeklyAdmissionDischarge { get; set; }
        public List<WeeklyWalkInPrescriptionIP> WeeklyWalkInPrescriptionIP { get; set; }
        public List<WeeklyPOGRNReturn> WeeklyPOGRNReturn { get; set; }
        public List<WeeklyOPRevenue> WeeklyOPRevenue { get; set; }
        public List<WeeklyIPRevenue> WeeklyIPRevenue { get; set; }
        public List<WeeklyPharmacyRevenue> WeeklyPharmacyRevenue { get; set; }
        public List<WeeklyGRNValueReturn> WeeklyGRNValueReturn { get; set; }

        public List<CollectionSummary> CollectionSummary { get; set; }
        public List<RevenueBillSummary> RevenueBillSummary { get; set; }
        public List<PurchaseDetailsSummary> PurchaseDetailsSummary { get; set; }
    }

    public class DailyPatientStatus
    {
        public long Registration { get; set; }
        public long Appointments { get; set; }
        public long Admission { get; set; }
        public long CurrentOccupancy { get; set; }
        public long WithMediclaim { get; set; }
        public long WithoutMediclaim { get; set; }
        public long ReferencePatients { get; set; }
        public long TotalPatients { get; set; }
    }

    public class DailyPaymentSummary
    {
        public decimal Collection { get; set; }
        public double Discount { get; set; }
        public decimal PendingDues { get; set; }
        public decimal Revenue { get; set; }
        public decimal Advances { get; set; }
        public decimal Refunds { get; set; }
        public long RXSale { get; set; }
        public long WalkingSale { get; set; }
        public long POClosed { get; set; }
        public long GRNCount { get; set; }
    }

    public class WeeklyOPTrend
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public long Registration { get; set; }
        public long Appointment { get; set; }
        public long New { get; set; }
        public long Old { get; set; }
    }

    public class WeeklyAdmissionDischarge
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public long AdmissionCount { get; set; }
        public long DischargeCount { get; set; }
    }

    public class WeeklyWalkInPrescriptionIP
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public long WalkInCount { get; set; }
        public long PrescriptionOpenCount { get; set; }
        public long PrescriptionClosedCount { get; set; }
        public long IPIssuedCount { get; set; }
    }

    public class WeeklyPOGRNReturn
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public long POCount { get; set; }
        public long GRNCount { get; set; }
        public long GRNReturnCount { get; set; }
    }

    public class WeeklyOPRevenue
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public decimal Rev { get; set; }
        public double Dis { get; set; }
        public decimal NetRev { get; set; }
    }

    public class WeeklyIPRevenue
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public decimal Rev { get; set; }
        public double Dis { get; set; }
        public decimal NetRev { get; set; }
    }

    public class WeeklyPharmacyRevenue
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public decimal Rev { get; set; }
        public decimal Dis { get; set; }
        public decimal NetRev { get; set; }
    }

    public class WeeklyGRNValueReturn
    {
        public DateTime ActivityDate { get; set; }
        public string DayName { get; set; }
        public decimal GRNValue { get; set; }
        public decimal GRNReturnValue { get; set; }
    }

    public class CollectionSummary
    {
        public string Collection { get; set; }
        public decimal Cash { get; set; }
        public decimal Card { get; set; }
        public decimal UPI { get; set; }
        public decimal BankTransfer { get; set; }
        public decimal Total { get; set; }
    }

    public class RevenueBillSummary
    {
        public string RevenueBill { get; set; }
        public decimal Gross { get; set; }
        public double Discount { get; set; }
        public decimal NetRevenue { get; set; }
    }

    public class PurchaseDetailsSummary
    {
        public string PurchaseDetails { get; set; }
        public decimal Amount { get; set; }
        public decimal UnverifiedAmount { get; set; }
        public decimal VerifiedAmount { get; set; }
    }
}
