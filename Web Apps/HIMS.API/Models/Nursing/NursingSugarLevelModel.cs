using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class NursingSugarLevelModel
    {
        public long Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryTime { get; set; }
        public long AdmissionId { get; set; }
        public string? Bsl { get; set; }
        public string? UrineSugar { get; set; }
        public string? Ettpressure { get; set; }
        public string? UrineKetone { get; set; }
        public string? Bodies { get; set; }
        public int? IntakeMode { get; set; }
        public string? ReportedToRmo { get; set; }
    }
}