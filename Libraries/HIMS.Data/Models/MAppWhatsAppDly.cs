namespace HIMS.Data.Models
{
    public partial class MAppWhatsAppDly
    {
        public int Id { get; set; }
        public decimal? Opcollection { get; set; }
        public decimal? Ipcollection { get; set; }
        public decimal? IpadvCollection { get; set; }
        public decimal? Oprefund { get; set; }
        public decimal? Iprefund { get; set; }
        public decimal? IpadvRefund { get; set; }
        public decimal PharAdvCollection { get; set; }
        public decimal PharAdvRefund { get; set; }
        public decimal PharCollection { get; set; }
        public decimal PharPhRefund { get; set; }
        public int AdmCnt { get; set; }
        public int VitCnt { get; set; }
    }
}
