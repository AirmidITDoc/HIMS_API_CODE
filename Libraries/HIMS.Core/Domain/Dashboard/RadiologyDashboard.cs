using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class RadiologyDashboard
    {
        public RadiologyCountSummary CountSummary { get; set; }
        public List<RadiologyVolume> RadiologyVolumes { get; set; }
        public List<RadiologyDailyTestCount> DailyTestCounts { get; set; }
        public List<RadiologyReport> RecentRadiologyReports { get; set; }
        public List<RadiologyOrderedTest> TopOrderedTests { get; set; }
        public List<RadiologyWorkload> RadiologyWorkloads { get; set; }
    }

    public class RadiologyCountSummary
    {
        public int TodaysCount { get; set; }
        public int CompletedCount { get; set; }
        public int PendingCount { get; set; }
        public int RejectedCount { get; set; }
    }

    public class RadiologyVolume
    {
        public string CategoryName { get; set; }
        public int CategoryCount { get; set; }
    }

    public class RadiologyDailyTestCount
    {
        public DateTime RadDate { get; set; }
        public int DayCount { get; set; }
    }

    public class RadiologyReport
    {
        public DateTime RadDate { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string PatientType { get; set; }
        public string? OPIPNumber { get; set; }
        public string IsCompleted { get; set; }
        public long RadTestID { get; set; }
        public string TestName { get; set; }
    }

    public class RadiologyOrderedTest
    {
        public string ServiceName { get; set; }
        public int Count { get; set; }
    }

    public class RadiologyWorkload
    {
        public string DoctorName { get; set; }
        public int Count { get; set; }
    }
}
