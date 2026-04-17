namespace HIMS.Services.Pharmacy
{
    public class PrescriptionDetListDto
    {
        public long OP_IP_ID { get; set; }
        public long ItemID { get; set; }
        public string ItemName { get; set; }
        public long Days { get; set; }
        public double QtyPerDay { get; set; }
        public double TotalQty { get; set; }
    }
}
