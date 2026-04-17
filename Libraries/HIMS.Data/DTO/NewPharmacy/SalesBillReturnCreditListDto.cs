namespace HIMS.Services.Pharmacy
{
    public class SalesBillReturnCreditListDto
    {
        public long SalesId { get; set; }
        public string SalesNo { get; set; }
        public long OP_IP_Type { get; set; }
        public long OP_IP_ID { get; set; }
        public long SalesDetId { get; set; }
        public string ItemName { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public decimal UnitMRP { get; set; }
        public double Qty { get; set; }
        public decimal TotalAmount { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double DiscPer { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal LandedPrice { get; set; }
        public decimal TotalLandedAmount { get; set; }
        public string ExternalPatientName { get; set; }
        public string DoctorName { get; set; }
        public bool IsBatchRequired { get; set; }
        public double ReturnQty { get; set; }
        public string PatientName { get; set; }
        public string RegNo { get; set; }
        public long IsPrescription { get; set; }
        public decimal TotalBillAmount { get; set; }
        public decimal TotalDiscAmount { get; set; }
        public decimal PurRateWf { get; set; }
        public decimal PurTotAmt { get; set; }
        public double CGSTPer { get; set; }
        public double SGSTPer { get; set; }
        public double IGSTPer { get; set; }
        public bool IsPurRate { get; set; }
        public long StkID { get; set; }
    }
}
