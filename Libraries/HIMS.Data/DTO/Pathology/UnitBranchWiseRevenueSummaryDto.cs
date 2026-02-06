using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class UnitBranchWiseRevenueSummaryDto
    {
        public string UnitBranchName { get; set; }
        public decimal TotalRevenue { get; set; }
        public double DiscountAmount { get; set; }
        public decimal NetRevenue { get; set; }
        public decimal BalAmount { get; set; }
        public double PatientCount { get; set; }

    }

    public class UnitBranchWiseTestSummaryDto
    {
        public string UnitBranchName { get; set; }
        public double TestCount { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public double TotalAmount { get; set; }
    }
    public class UnitCategoryTestSummaryDto
    {
        public string UnitBranchName { get; set; }
        public string CategoryName { get; set; }
        public long TestCount { get; set; }
        public double TotalAmount { get; set; }
    }
    public class UnitDoctorTestSummaryDto
    {
        public string? UnitBranchName { get; set; }
        public string? DoctorName { get; set; }
        public int TestCount { get; set; }
        public double TotalAmount { get; set; }
    }
    public class UnitCompanyTestSummaryDto
    {
        public string? UnitBranchName { get; set; }
        public string? CompanyName { get; set; }
        public int TestCount { get; set; }
        public double TotalAmount { get; set; }
    }

}
