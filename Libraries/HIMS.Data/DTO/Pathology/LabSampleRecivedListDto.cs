using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public  class LabSampleRecivedListDto
    {
        public long LabPatientId { get; set; }
        public string? DoctorName { get; set; }
        public string? LBL { get; set; }
        public string? PBillNo { get; set; }
        public bool? IsSampleCollection { get; set; }
        public long? PathReportID { get; set; }
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
        public byte? OPD_IPD_Type { get; set; }
        public string? GenderName { get; set; }
        public string? HospitalName { get; set; }
        public DateTime? SampleCollectionTime { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public DateTime? OutSourceSampleSentDateTime { get; set; }
        public string? OutSourceStatus { get; set; }
        public DateTime? OutSourceReportCollectedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public DateTime? OutSourceCreatedDateTime { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public DateTime? OutSourceModifiedDateTime { get; set; }
        public DateTime? SampleReceviedDateTime { get; set; }
        public long SampleReceviedUserId { get; set; }
        public bool? IsSampleReceivedStatus { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsPathOutSource { get; set; }
        public bool? IsRadOutSource { get; set; }
        public DateTime? TestTime { get; set; }
        public DateTime? TestDate { get; set; }
        public long? SpecimenTypeId { get; set; }
        public string? SpecimenColor { get; set; }
        public long? SpecimenQty { get; set; }
        public long? SpecimenConditionId { get; set; }
        public long? ContainerTypeId { get; set; }
        public long? CollectionMethod { get; set; }
        public long? NoofContainer { get; set; }
        public long? PreservationUsed { get; set; }
        public string? BarcodeLabel { get; set; }
        public string? ConsentDetail { get; set; }
        public bool? IsConsentRequired { get; set; }
        public bool? IsFastingRequired { get; set; }
        public bool? IsApprovedRequired { get; set; }
        public string? TestInformationTemplate { get; set; }
        public long? TATDay { get; set; }
        public long? TATHour { get; set; }
        public long? TATMin { get; set; }
        public string? SpecimenColorName { get; set; }
        public string? SpecimenTypeName { get; set; }
        public string? SpecimenCondition { get; set; }
        public string? ContainerType { get; set; }
        public string? CollectionMethodName { get; set; }
        public string? PreservationUsedName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? UserName { get; set; }
        public string? SamplecollectedBy { get; set; }
        public bool? IsSampleReceviedCancel { get; set; }
        public string? SampleReceviedCancelReason { get; set; }
        public long? SampleReceviedCanceledBy { get; set; }
        public bool? IsCompleted { get; set; }



    }
}
