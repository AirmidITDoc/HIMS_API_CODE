namespace HIMS.Data.Models
{
    public partial class BarcodeConfigMaster
    {
        public long Id { get; set; }
        public string TemplateCode { get; set; } = null!;
        public string Width { get; set; } = null!;
        public string Height { get; set; } = null!;
        public string TemplateBody { get; set; } = null!;
        public string? Padding { get; set; }
        public string? Margin { get; set; }
        public bool IsActive { get; set; }
        public string? BarcodeData { get; set; }
    }
}
