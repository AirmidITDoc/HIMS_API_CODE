namespace HIMS.Data.Models
{
    public partial class MSystemConfig
    {
        public long SystemConfigId { get; set; }
        public long? SystemCategoryId { get; set; }
        public string? SystemName { get; set; }
        public long? IsInputField { get; set; }
        public string? SystemInputValue { get; set; }
    }
}
