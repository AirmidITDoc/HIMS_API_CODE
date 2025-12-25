namespace HIMS.Data.DTO.Pathology
{
    public class PathTestForUpdateListdto
    {
        public long SubTestID { get; set; }
        public long ParameterID { get; set; }
        public string? ParameterName { get; set; }
        public bool IsSubTest { get; set; }
        public long TestId { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public string? MethodName { get; set; }
        public string? ParaMultipleRange { get; set; }
        public string? UnitName { get; set; }

        public bool? IsActive { get; set; }


    }
}
