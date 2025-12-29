namespace HIMS.Data.DTO.Administration
{
    public partial class MParameterDescriptiveMasterListDto
    {
        public long DescriptiveId { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterValues { get; set; }
        public bool? IsDefaultValue { get; set; }
        public string? DefaultValue { get; set; }
    }
}
