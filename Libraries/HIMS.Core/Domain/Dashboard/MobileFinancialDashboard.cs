using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
   
        public class MobileFinancialDashboard
        {
            public List<ServiceWiseCharges> ServiceWiseCharges { get; set; }
            public PaymentModeSummary PaymentModeSummary { get; set; }
            public ReceiptAdvanceSummary ReceiptAdvanceSummary { get; set; }
            public List<DoctorWiseBusiness> DoctorWiseBusiness { get; set; }
            public List<GroupWiseCollection> GroupWiseCollection { get; set; }
            public PharmacySaleSummary PharmacySaleSummary { get; set; }
            public List<MobilePackagePatientCount> PackagePatientCount { get; set; }
            public OutstandingSummary OutstandingSummary { get; set; }
            public UnadjustedAdvanceSummary UnadjustedAdvanceSummary { get; set; }
            public InsuranceSummary InsuranceSummary { get; set; }
        }

        public class ServiceWiseCharges
        {
            public string ServiceName { get; set; }
            public decimal TotalAmt { get; set; }
            public decimal Discount { get; set; }
            public decimal NetAmount { get; set; }
        }

        public class PaymentModeSummary
        {
        public decimal Cash { get; set; }
        public decimal Cheque { get; set; }
        public decimal CardPay { get; set; }
        public decimal NEFT { get; set; }
        public decimal UPI { get; set; }
        public decimal UsedAdvance { get; set; }
    }

        public class ReceiptAdvanceSummary
        {
        public decimal TotalCollection { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal AdvusedAmount { get; set; }
        public decimal AdvRefundAmount { get; set; }
        public decimal Receipt { get; set; }
        public decimal Advance { get; set; }
        public decimal Refund { get; set; }
        public decimal PharmacyReturn { get; set; }
    }

        public class DoctorWiseBusiness
        {
        public string DoctorName { get; set; }
        public long PatientCount { get; set; }
        public decimal TotalBusiness { get; set; }
        public decimal ConsultationFee { get; set; }
        public double DoctorShare { get; set; }
    }

        public class GroupWiseCollection
        {
        public string GroupName { get; set; }
        public decimal TotalAmount { get; set; }
    }

        public class PharmacySaleSummary
        {
        public decimal TotalLandedAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal ProfitAmount { get; set; }
    }

        public class MobilePackagePatientCount
        {
        public string PackageName { get; set; }
        public long PatientCount { get; set; }
    }

        public class OutstandingSummary
        {
        public decimal TotalOutstanding { get; set; }
        public decimal OutstandingInPeriod { get; set; }
    }

        public class UnadjustedAdvanceSummary
        {
        public double UnadjustedAdvance { get; set; }
    }

        public class InsuranceSummary
        {
        public decimal IP_ApprovedAmount { get; set; }
        public decimal IP_UnpaidCharges { get; set; }
    }
    }

