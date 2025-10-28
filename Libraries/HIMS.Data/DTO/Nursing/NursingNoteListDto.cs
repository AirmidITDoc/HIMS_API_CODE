namespace HIMS.Data.DTO.Nursing
{
    public class NursingNoteListDto
    {
        public long DocNoteId { get; set; }
        public long AdmID { get; set; }
        public string TDate { get; set; }
        public string TTime { get; set; }
        public string NursingNotes { get; set; }
        public long IsAddedBy { get; set; }
        public string VTDate { get; set; }
        public string PatientName { get; set; }
        public string GenderName { get; set; }
        public string Age { get; set; }
        public string? RegNo { get; set; }
        public string? UserName { get; set; }


    }
}
