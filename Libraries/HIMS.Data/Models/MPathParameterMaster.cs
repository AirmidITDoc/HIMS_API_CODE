namespace HIMS.Data.Models
{
    public partial class MPathParameterMaster
    {
        public MPathParameterMaster()
        {
            MParameterDescriptiveMasters = new HashSet<MParameterDescriptiveMaster>();
            MPathParaRangeWithAgeMasters = new HashSet<MPathParaRangeWithAgeMaster>();
            MPathTestDetailMasters = new HashSet<MPathTestDetailMaster>();
        }

        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
        public string? ParameterName { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public long? IsNumeric { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsPrintDisSummary { get; set; }
        public string? MethodName { get; set; }
        public string? ParaMultipleRange { get; set; }
        public string? Formula { get; set; }
        public string? IsBoldFlag { get; set; }
        public string? ParaBoldFlag { get; set; }
        public bool? IsBold { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MParameterDescriptiveMaster> MParameterDescriptiveMasters { get; set; }
        public virtual ICollection<MPathParaRangeWithAgeMaster> MPathParaRangeWithAgeMasters { get; set; }
        public virtual ICollection<MPathTestDetailMaster> MPathTestDetailMasters { get; set; }
    }
}
