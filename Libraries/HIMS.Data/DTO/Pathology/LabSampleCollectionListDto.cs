using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabSampleCollectionListDto
    {
        public long LabPatientId { get; set; }
        public string? DoctorName { get; set; }
        public string? LBL { get; set; }
        public string? PBillNo { get; set; }
        public bool? IsSampleCollection { get; set; }
        public string? SampleNo { get; set; }
        public string? BillNo { get; set; }
        public DateTime? PathDate { get; set; }
        public string? WardName { get; set; }
        public string? DepartmentName { get; set; }
        public string? PatientType { get; set; }
        public DateTime? VADate { get; set; }
        public DateTime? VATime { get; set; }
        public string? LabRequestNo { get; set; }
        public string? PatientName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? MobileNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? OPD_IPD_Type { get; set; }
        public string? GenderName { get; set; }
        // From Expr columns (usually name split or extra fields)
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? HospitalName { get; set; }
        public string ServiceNames { get; set; }
        public string SampleCollectionTime { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public DateTime? OutSourceSampleSentDateTime { get; set; }
        public long? OutSourceStatus { get; set; }
        public DateTime? OutSourceReportCollectedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public DateTime? OutSourceCreatedDateTime { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public DateTime? OutSourceModifiedDateTime { get; set; }
        public long? PathReportID { get; set; }
        public string? IsPathOutSource { get; set; }
        public string? IsRadOutSource { get; set; }
        public long? PathTestID { get; set; }
        public DateTime? TestTime { get; set; }
        public DateTime? TestDate { get; set; }
        public long? SpecimenTypeId { get; set; }
        public long? SpecimenColor { get; set; }
        public long? SpecimenQty { get; set; }
        public long? SpecimenConditionId { get; set; }
        public long? ContainerTypeId { get; set; }
        public long? CollectionMethod { get; set; }
        public string? BarcodeLabel { get; set; }
        public string? ConsentDetail { get; set; }
        public bool? IsConsentRequired { get; set; }
        public bool? IsFastingRequired { get; set; }
        public bool? IsApprovedRequired { get; set; }
        public string? TestInformationTemplate { get; set; }
        public long? Tatday { get; set; }
        public long? Tathour { get; set; }
        public long? Tatmin { get; set; }




    }
    public class LabSampleCollectionDetailListDto
    {
        public long PathTestID { get; set; }
        public string? ServiceName { get; set; }
        public long? PathReportID { get; set; }
        public long? ServiceId { get; set; }
        public string? DOA { get; set; }
        public string? DOT { get; set; }
        public string? OP_IP_No { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long? Visit_Adm_ID { get; set; }
        public string? OPD_IPD_Type { get; set; }
        public string? PatientType { get; set; }
        public string? SampleNo { get; set; }
        public string? SampleCollectionTime { get; set; }
        public bool? IsSampleCollection { get; set; }
        public string? BillNo { get; set; }
        public string? UserName { get; set; }
        public int? IsApprovedByCamp { get; set; }
        public int? PatientTypeID { get; set; }
    }
}
