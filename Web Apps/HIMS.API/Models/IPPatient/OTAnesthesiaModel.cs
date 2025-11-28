using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.IPPatient
{
    public class OTAnesthesiaModel
    {
        public long AnesthesiaId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? AnesthesiaDate { get; set; }
        public string? AnesthesiaTime { get; set; }
        public string? AnesthesiaNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public DateTime? AnesthesiaStartDate { get; set; }
        public string? AnesthesiaStartTime { get; set; }
        public DateTime? AnesthesiaEndDate { get; set; }
        public string? AnesthesiaEndTime { get; set; }
        public DateTime? RecoveryStartDate { get; set; }
        public string? RecoveryStartTime { get; set; }
        public DateTime? RecoveryEndDate { get; set; }
        public string? RecoveryEndTime { get; set; }
        public long? AnesthesiaType { get; set; }
        public string? AnesthesiaNotes { get; set; }
        public List<TOtAnesthesiaPreOpdiagnosisModel> TOtAnesthesiaPreOpdiagnoses { get; set; }

    }
    public class TOtAnesthesiaPreOpdiagnosisModel
    {
        public long OtanesthesiaPreOpdiagnosisId { get; set; }
        public long? AnesthesiaId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }


    }
}
