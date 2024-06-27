using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO
{
    public class DailyDashboardSummaryModel
    {
        public string Title { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public int Count { get; set; }
        public string SelfLbl { get; set; } = string.Empty;
        public int SelfCnt { get; set; }
        public string CompanyLbl { get; set; } = string.Empty;
        public int CompayCnt { get; set; }
    }

    public class OPDepartmentRangeChartRequestModel
    {
        public string DateRange { get; set; } = string.Empty;
    }

    public class OPDepartmentRangeChartModel
    {
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
        public int TotalCount { get; set; }
        public int DischargeCount { get; set; }
    }
    public class IPAdemissionDischargeCountModel
    {
        public int AppointmentCount { get; set; }
        public int TotalAdmittedPatientCount { get; set; }
        public int SelfPatient { get; set; }
        public int CompnayPatient { get; set; }
        public int TodayAdmittedPatient { get; set; }
        public int TodayDischargePatient { get; set; }
        public int TodaySelfPatient { get; set; }
        public int TodayOtherPatient { get; set; }
    }

    public class OPVisitCountRequestModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class OPVisitCountList
    {
        public int CompanyPatientCount { get; set; }
        public int CrossConsultantPatCount { get; set; }
        public int NewPatientCount { get; set; }
        public int OldPatientCount { get; set; }
        public int TotalVisitCount { get; set; }
    }
}
