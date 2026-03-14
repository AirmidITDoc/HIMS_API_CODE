namespace HIMS.API.Models.Nursing
{
    public class IpdDrugScheduleModel
    {
        public long IpdDrugScheduleId { get; set; }
        public long AdmissionId { get; set; }
        public long DrugId { get; set; }
        public int DoseNo { get; set; }
        public DateTime DoseTime { get; set; }
        public int Status { get; set; }
        public string? Comment { get; set; }
    }
}
