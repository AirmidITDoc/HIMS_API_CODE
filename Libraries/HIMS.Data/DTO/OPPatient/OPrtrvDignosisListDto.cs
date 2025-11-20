namespace HIMS.Data.DTO.OPPatient
{
    public class OPrtrvDignosisListDto
    {
        public string DescriptionType { get; set; }
        public long Id { get; set; }
        public string DescriptionName { get; set; }
    }
    public class TOtRequestDiagnosisListDto
    {
        public long OtrequestDiagnosisDetId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
    }
    public class ReservationDiagnosisListDto
    {
        public long OtreservationDiagnosisDetId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
    }
    public class PrescriptionDignosisListDto
    {
        public string? TemplateCategory { get; set; }
        public string? PresTemplateName { get; set; }
        public long PresId { get; set; }
    }


}
