namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionReturnDto
    {
        public long PresReId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double? Qty { get; set; }
    }
}
