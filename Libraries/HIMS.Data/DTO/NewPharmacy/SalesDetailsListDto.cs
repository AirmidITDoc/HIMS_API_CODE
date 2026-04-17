namespace HIMS.Services.Pharmacy
{
    public class SalesDetailsListDto
    {
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public decimal UnitMRP { get; set; }
        public double Qty { get; set; }
        public decimal TotalAmount { get; set; }
        public double DiscPer { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public long OP_IP_Type { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double CGSTPer { get; set; }
        public decimal CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public decimal SGSTAmt { get; set; }
        public double IGSTPer { get; set; }
        public decimal IGSTAmt { get; set; }
        public long SalesId { get; set; }
        public bool IsPurRate { get; set; }

    }
}
