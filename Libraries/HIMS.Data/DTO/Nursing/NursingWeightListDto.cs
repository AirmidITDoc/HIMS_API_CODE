namespace HIMS.Data.DTO.Nursing
{
    public class NursingWeightListDto
    {
        public string alterdBy { get; set; }
        public string ModifiedBy { get; set; }
        public long PatWeightId { get; set; }
        public DateTime? PatWeightDate { get; set; }
        public DateTime? PatWeightTime { get; set; }
        public long? AdmissionId { get; set; }
        public int? PatWeightValue { get; set; }
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
