namespace HIMS.Services.Pharmacy
{
    public class SalesReturnDetailsListDto
    {
        public string ItemName { get; set; }
        public long SalesReturnId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public decimal UnitMRP { get; set; }
        public double Qty { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double CGSTPer { get; set; }
        public decimal CGSTAmt { get; set; }
        public double SGSTPer { get; set; }
        public decimal SGSTAmt { get; set; }
        public double IGSTPer { get; set; }
        public decimal IGSTAmt { get; set; }
    }
}
