namespace HIMS.Data.DTO.Pathology
{
    public class SubTestMasterListDto
    {
        public long? TestId { get; set; }
        public string? ParameterName { get; set; }
        public string? SubTestID { get; set; }
        public long? ParameterId { get; set; }
    }
    public class PathTestDetailDto
    {
        public long? TestId { get; set; }
        public long? ParameterId { get; set; }
        public string? TestName { get; set; }
        public string? ParameterName { get; set; }
    }
}
