namespace HIMS.Data.Models
{
    public partial class TNursingSugarLevel
    {
        public long Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime EntryTime { get; set; }
        public long AdmissionId { get; set; }
        public string? Bsl { get; set; }
        public string? UrineSugar { get; set; }
        public string? Ettpressure { get; set; }
        public string? UrineKetone { get; set; }
        public string? Bodies { get; set; }
        public int? IntakeMode { get; set; }
        public string? ReportedToRmo { get; set; }
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
