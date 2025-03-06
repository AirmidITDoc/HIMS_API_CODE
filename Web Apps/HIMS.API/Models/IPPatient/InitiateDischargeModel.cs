namespace HIMS.API.Models.IPPatient
{
    public class InitiateDischargeModel
    {
        public long InitateDiscId { get; set; }
        public long? AdmId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DepartmentId { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTime? ApprovedDatetime { get; set; }
        public bool? IsNoDues { get; set; }
        public string? Comments { get; set; }
    }
}
