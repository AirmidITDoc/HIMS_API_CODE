namespace HIMS.API.Models
{
    public class LicenseModel
    {
        public string Customer { get; set; } = "";
        public string MachineHash { get; set; } = "";
        public DateTime ExpiryDate { get; set; } = default!;
    }
}
