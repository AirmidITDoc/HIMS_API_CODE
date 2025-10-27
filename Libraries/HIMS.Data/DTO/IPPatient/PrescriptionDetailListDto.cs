namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionDetailListDto
    {
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public long MedicalRecoredId { get; set; }
        public long IPMedID { get; set; }
        public long OP_IP_ID { get; set; }
        public bool IsClosed { get; set; }

    }
}
