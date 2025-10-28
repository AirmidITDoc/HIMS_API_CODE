namespace HIMS.Data.Models
{
    public partial class MParameterDescriptiveMaster
    {
        public long DescriptiveId { get; set; }
        public long ParameterId { get; set; }
        public string? ParameterValues { get; set; }
        public bool IsDefaultValue { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? DefaultValue { get; set; }

        public virtual MPathParameterMaster Parameter { get; set; } = null!;
    }
}
