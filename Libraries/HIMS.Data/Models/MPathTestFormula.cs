namespace HIMS.Data.Models
{
    public partial class MPathTestFormula
    {
        public long FormulaId { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterName { get; set; }
        public string? Formula { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
