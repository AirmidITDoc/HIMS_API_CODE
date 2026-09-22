namespace HIMS.Data.DTO.OPPatient
{
    public class PrevDrVisistListDto
    {
        public string? DepartmentName { get; set; }
        public string? DoctorName { get; set; }
        public long RegID { get; set; }
        public long DepartmentId { get; set; }
        public long ConsultantDocId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime RegDate { get; set; }
        public long CampId { get; set; }
        public long VisitId { get; set; }
        public long? LabPatientId { get; set; }
        public string OPDNo { get; set; }
    }

    public class PrevOPDrVisistListDto
    {
        public string? DepartmentName { get; set; }
        public string? DoctorName { get; set; }
        public long RegID { get; set; }
        public long DepartmentId { get; set; }
        public long ConsultantDocId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime RegDate { get; set; }
        public long CampId { get; set; }
        public long VisitId { get; set; }
        public long? LabPatientId { get; set; }
        public string OPDNo { get; set; }
        public long AbhaTranId { get; set; }
        public string AbhaNumber { get; set; } = null!;
        public string AbhaFullName { get; set; } = null!;
        public string AbhaAddress { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime YearOfBirth { get; set; }
        public long Peccid { get; set; }
    }
}
