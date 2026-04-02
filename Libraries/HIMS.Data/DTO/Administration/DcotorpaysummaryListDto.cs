namespace HIMS.Data.DTO.Administration
{
    public class DcotorpaysummaryListDto
    {
        public string? AddChargeDrName { get; set; }
        public decimal? NetAmount { get; set; }
        public double? DocAmt { get; set; }
        public decimal? HospitalAmt { get; set; }
        public long DoctorId { get; set; }
    }
}
