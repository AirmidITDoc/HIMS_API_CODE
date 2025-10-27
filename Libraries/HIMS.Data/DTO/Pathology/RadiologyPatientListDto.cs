namespace HIMS.Data.DTO.Pathology
{
    public class RadiologyPatientListDto
    {
        public long RadReportId { get; set; }
        public string? RadDate { get; set; }
        public string? RadTime { get; set; }
        public long Visit_Adm_ID { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public string? DoctorName { get; set; }
        public string? TestName { get; set; }
        public bool OPD_IPD_Type { get; set; }
        public string? PBillNo { get; set; }
        public string? ServiceName { get; set; }
        public bool IsCompleted { get; set; }
        public string? IsPrinted { get; set; }
        public string? OP_IP_Number { get; set; }
        public long RadTestID { get; set; }
        public long TestId { get; set; }
        public long ChargeId { get; set; }
        public string? CategoryName { get; set; }
        public long OPD_IPD_ID { get; set; }
        public string? LBL { get; set; }
        public string? AgeYear { get; set; }
        public string? DepartmentName { get; set; }
        public string? CompanyName { get; set; }
        public bool? PatientType { get; set; }
        public string? RefDoctorName { get; set; }
        public long ServiceId { get; set; }
        public string? MobileNo { get; set; }


    }
}

