using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class PathologyDashboard
    {
        public PathologyCountSummary CountSummary { get; set; }
        public List<PathologyValume> PathologyValumes { get; set; }
        public List<DailyTestCount> DailyTestCounts { get; set; }
        public List<PathologyReport> RecentPathologyReports { get; set; }
        public List<PathologyOrderedTest> MostOrderedTests { get; set; }
        public List<PathologyWorkload> PathologyWorkloads { get; set; }
    }
    public class PathologyCountSummary
    {
        public int TodaysCount { get; set; }
        public int CompletedCount { get; set; }
        public int PendingCount { get; set; }
        public int RejectedCount { get; set; }
    }
    public class PathologyValume
    {
        public string CategoryName { get; set; }
        public int CategoryCount { get; set; }
    }
    public class DailyTestCount
    {
        public DateTime PathDate { get; set; }
        public int DayCount { get; set; }
    }
    public class PathologyReport
    {
        public DateTime PathDate { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string PatientType { get; set; }
        public string OPIPNumber { get; set; }
        public string IsCompleted { get; set; }
        public long PathTestID { get; set; }
        public string TestName { get; set; }
        public string PrintTestName { get; set; }
    }
    public class PathologyOrderedTest
    {
        public string TestName { get; set; }
        public int Count { get; set; }
    }
    public class PathologyWorkload
    {
        public string DoctorName { get; set; }
        public int Count { get; set; }
    }
}
