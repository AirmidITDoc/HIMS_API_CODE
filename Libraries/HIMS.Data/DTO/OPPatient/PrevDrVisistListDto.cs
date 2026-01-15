namespace HIMS.Data.DTO.OPPatient
{
    public class PrevDrVisistListDto
    {
        public string? DepartmentName { get; set; }
        public string? DoctorName { get; set; }
        public long? RegID { get; set; }
        public long? DepartmentId { get; set; }
        public long? ConsultantDocId { get; set; }
        public DateTime VisitDate { get; set; }

        public DateTime RegDate { get; set; }

        public long CampId { get; set; }
        public long? LabPatientId { get; set; }
    }
}
