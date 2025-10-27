namespace HIMS.Data.Models
{
    public partial class TNursingNote
    {
        public long DocNoteId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Tdate { get; set; }
        public DateTime? Ttime { get; set; }
        public string? NursingNotes { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
