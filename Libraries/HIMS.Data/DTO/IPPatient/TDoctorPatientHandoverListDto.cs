namespace HIMS.Data.DTO.IPPatient
{
    public class TDoctorPatientHandoverListDto
    {
        public long DocHandId { get; set; }
        public long AdmID { get; set; }
        public DateTime TDate { get; set; }
        public string VDate { get; set; }
        public string MTime { get; set; }
        public string ShiftInfo { get; set; }
        public string PatHandI { get; set; }
        public string? PatHandS { get; set; }
        public string? PatHandB { get; set; }
        public string? PatHandA { get; set; }
        public string? PatHandR { get; set; }
        public string CreatedBy { get; set; }
        public string PatientName { get; set; }
        public string GenderName { get; set; }

    }
}
