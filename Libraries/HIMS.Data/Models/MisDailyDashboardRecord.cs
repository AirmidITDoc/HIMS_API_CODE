using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MisDailyDashboardRecord
    {
        public DateTime? BillDate { get; set; }
        public int? BillDay { get; set; }
        public int? BillWeek { get; set; }
        public int? BillMonth { get; set; }
        public int? BillQuarter { get; set; }
        public int? BillYear { get; set; }
        public string? OpdIpd { get; set; }
        public int? BBilledPatientCount { get; set; }
        public int? BDistinctBilledPatientCount { get; set; }
        public string? Department { get; set; }
        public string? RefProcessedDate { get; set; }
        public string? RefDocName { get; set; }
        public string? PatientSource { get; set; }
        public int? LengthOfStay { get; set; }
        public string? AdmDoctor { get; set; }
        public string? SurgeryName { get; set; }
        public string? Company { get; set; }
        public string? Country { get; set; }
        public string MedicineOperative { get; set; } = null!;
        public decimal? BTotalBillAmountSum { get; set; }
        public decimal? BTotalConcessionAmountSum { get; set; }
        public decimal? BBalanceBilledAmount { get; set; }
        public decimal? BBilledPaidAmount { get; set; }
        public decimal? HospitalExpences { get; set; }
        public decimal? PharmaOt { get; set; }
        public decimal? PackageActShare { get; set; }
        public decimal? PaidDoctorshare { get; set; }
        public decimal? PaidrefDoctorshare { get; set; }
        public decimal? AdmShare { get; set; }
        public decimal? PackageAdmShare { get; set; }
        public decimal? AdmSharetotal { get; set; }
        public decimal? PathoInHouse { get; set; }
        public decimal? PathoOutsource { get; set; }
        public decimal? RadioInHouse { get; set; }
        public decimal? RadioOutsource { get; set; }
        public string? RecordInsertedBy { get; set; }
        public string? RecordUpdatedBy { get; set; }
        public DateTime? RecordInsertedOn { get; set; }
        public DateTime? RecordUpdatedOn { get; set; }
    }
}
