namespace HIMS.Services.Users
{
    public class PharSalesListDto
    {
        public string SalesNo { get; set; }
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string MobileNo { get; set; }
        public double Qty { get; set; }
        public decimal TotalAmount { get; set; }
    }
}