namespace HIMS.Data.DTO.Pathology
{
    public class SampleCollectionPatientListDto
    {
        public string RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? OP_IP_No { get; set; }
        public string VADate { get; set; }
        public string VATime { get; set; }
        public string DOA { get; set; }
        public string? DOT { get; set; }
        public string DoctorName { get; set; }
        public long? Visit_Adm_ID { get; set; }
        public string? LBL { get; set; }
        public string? PBillNo { get; set; }
        public string? IsSampleCollection { get; set; }
        public string? BillNo { get; set; }
        public DateTime? PathDate { get; set; }
        public string? PathTime { get; set; }

        public string? WardName { get; set; }
        public string? CompanyName { get; set; }
        public string? PatientType { get; set; }
        public string? DepartmentName { get; set; }

    }
}
