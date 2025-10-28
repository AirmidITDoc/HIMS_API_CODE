namespace HIMS.Data.DTO.Pathology
{
    public class PathTestForUpdateListdto
    {
        public long SubTestID { get; set; }
        public long ParameterID { get; set; }
        public string? ParameterName { get; set; }
        public bool IsSubTest { get; set; }
        public long TestId { get; set; }
        public bool? IsActive { get; set; }


    }
}
