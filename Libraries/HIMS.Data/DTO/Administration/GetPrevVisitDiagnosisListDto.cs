namespace HIMS.Data.DTO.Administration
{
    public class GetPrevVisitDiagnosisListDto
    {
        public long VisitId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
        public long RegID { get; set; }


    }
}
