using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class HomeCollectionDashboard
    {
        public List<PhlebotomistCountListModel> PhlebotomistCountListModel { get; set; }
        public List<HomeCollectionBillingStatusModel> HomeCollectionBillingStatusModel { get; set; }
        public List<HomeCollectionBillGroupWiseCountModel> HomeCollectionBillGroupWiseCountModel { get; set; }
        public List<HomeCollectionCenterWiseCountModel> homeCollectionCenterWiseCountModels { get; set; }
        //public List<HomeCollectionCountSummaryModel> HomeCollectionCountSummaryModel { get; set; }
    }
    public class PhlebotomistCountListModel
    {
        public string PhlebotomistName { get; set; }
        public long TotalCount { get; set; }
        public long Assigned { get; set; }
        public long Pending { get; set; }
    }

    public class HomeCollectionBillingStatusModel
    {
        public string PhlebotomistName { get; set; }
        public long TotalCount { get; set; }
        public long Assigned { get; set; }
        public long Pending { get; set; }
        public long BillCompleted { get; set; }
    }

    public class HomeCollectionBillGroupWiseCountModel
    {
        public string GroupName { get; set; }
        public long CategoryWiseCount { get; set; }
    }

    //public class HomeCollectionCountSummaryModel
    //{
    //    public long TodayCount { get; set; }
    //    public long ScheduledCount { get; set; }
    //    public long CollectedCount { get; set; }
    //    public long pendingCount { get; set; }
    //}

    public class HomeCollectionCenterWiseCountModel
    {
        public string CenterName { get; set; }
        public long CenterWiseCount { get; set; }
    }
}
