namespace HIMS.Data.DTO.Pathology
{
    public class PatientClearanceAprovViewListDto
    {
        public long InitateDiscId { get; set; }
        public long AdmID { get; set; }
        public string? DepartmentName { get; set; }
        public long DepartmentID { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }
        public string? ApprovedDatetime { get; set; }
        public string? ApprovedByName { get; set; }
        public bool? IsNoDues { get; set; }
        public string? Comments { get; set; }


    }

}
