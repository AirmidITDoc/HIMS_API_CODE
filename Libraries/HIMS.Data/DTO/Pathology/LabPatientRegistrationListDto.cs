using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabPatientRegistrationListDto
    {
        public long LabPatientId { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public string? LabRequestNo { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? MobileNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public string? CityName { get; set; }
        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public string? UserName { get; set; }
        public string? HospitalName { get; set; }
        public long TotalAmount { get; set; }
        public long DiscAmount { get; set; }
        public long NetAmount { get; set; }
        public long PaidAmount { get; set; }
        public string? PBillNo { get; set; }
        public long IsCancelled { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? OnlinePay { get; set; }
        public string? BillNo { get; set; }
        public string? companyName { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public long? CompanyId { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? PayTMAmount { get; set; }
        public string? PatientType { get; set; }
        public string? PatientType1 { get; set; }
        public long? TariffId { get; set; }
        public string? Comments { get; set; }
        public string? ReferByName { get; set; }
        public long? CompanyExecutiveId { get; set; }
        public string? ExecutiveName { get; set; }
        public long? PatientTypeId { get; set; }
        public long? PatientTypeId1 { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime CreatedDate { get; set; }

        










    }

    public class LabVisitDetailsListSearchDto
    {
        public string? FirstName { get; set; }
        public string? RegNo { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long VisitId { get; set; }
        public long? RegId { get; set; }
        public string FormattedText { get { return this.FirstName + " " + this.MiddleName + " " + this.LastName + " | " + this.MobileNo + " | " + this.RegNo; } }
        public string? MobileNo { get; set; }
        public long? PatientTypeId { get; set; }
        public long? ConsultantDocId { get; set; }
        public long? RefDocId { get; set; }
        public string? OPDNo { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public string? TariffName { get; set; }
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? DoctorName { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public long? UnitId { get; set; }
        public long? hospitalId { get; set; }
        public long LabPatRegId { get; set; }
        public long LabPatientId { get; set; }

        

    }
}
