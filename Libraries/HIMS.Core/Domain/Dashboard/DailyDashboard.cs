using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class DailyDashboard
    {
        public List<DailyTrend> Trend { get; set; }
        public PatientSummary PatientSummary { get; set; }
        public DashboardSummary DashboardSummary { get; set; }
        public PaymentOverview PaymentOverview { get; set; }
    }
    public class DailyTrend
    {
        public string DayName { get; set; }
        public long OPD { get; set; }
        public long IPD { get; set; }
    }

    public class PatientSummary
    {
        public long WithMediclaim { get; set; }
        public long WithoutMediclaim { get; set; }
        public long ReferencePatients { get; set; }
        public long TotalPatients { get; set; }
    }

    public class DashboardSummary
    {
        public decimal TodayRevenue { get; set; }
        public decimal PendingDues { get; set; }
        public decimal Refunds { get; set; }
        public decimal Advances { get; set; }
    }

    public class PaymentOverview
    {
        public decimal Cash { get; set; }
        public decimal Online { get; set; }
        public decimal Card { get; set; }
        public decimal Cheque { get; set; }
    }
}
