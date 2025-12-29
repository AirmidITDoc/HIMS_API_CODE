namespace HIMS.Data.DTO.Pathology
{
    public class RadiologyListDto
    {
        public long RadReportId { get; set; }
        public string? RadDate { get; set; }
        public string? RadTime { get; set; }
        public long VisitId { get; set; }
        public long Visit_Adm_ID { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }

        public string? MobileNo { get; set; }

        public string? AadharCardNo { get; set; }
        public string? ConsultantDoctor { get; set; }
        public string? VisitDate { get; set; }
        public string? VisitTime { get; set; }
        public string? TestName { get; set; }
        public long opdipdtype { get; set; }
        public string? PBillNo { get; set; }
        public string? BillNo { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public string OPDNo { get; set; }
        public long RadTestID { get; set; }
        public long TestId { get; set; }
        public long ChargeId { get; set; }
        public string? CategoryName { get; set; }
        public long IsCancelled { get; set; }
        public string? AdmissionDate { get; set; }
        public string? OP_IP_Number { get; set; }
        public long opdipdid { get; set; }
        public string PatientType { get; set; }
        public string AgeYear { get; set; }
        public bool? IsRadOutSource { get; set; }
        public bool? IsVerifySign { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? IsVerifyedDate { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public DateTime? OutSourceSampleSentDateTime { get; set; }
        public long? OutSourceStatus { get; set; }
        public DateTime? OutSourceReportCollectedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public DateTime? OutSourceCreatedDateTime { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public DateTime? OutSourceModifiedDateTime { get; set; }
        public string VerifiedUserName { get; set; }


    }

}




