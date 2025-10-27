namespace HIMS.API.Models.OutPatient
{
    public class PhBillDiscountAfterModel
    {
        public long? SalesId { get; set; }
        public double? NetAmount { get; set; }
        public double? DiscAmount { get; set; }
        public double? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }

    }

    public class GlobalDiscountModel
    {
        public long? SalesId { get; set; }
        public double? NetAmount { get; set; }
        public double? DiscAmount { get; set; }
        public double? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }

    }
    public class GlobalDiscountModels
    {
        public List<GlobalDiscountModel> Sales { get; set; }


    }


    public class ExtpatientDetModel
    {
        public string? ExtMobileNo { get; set; }
        public string? ExternalPatientName { get; set; }
        public string? ExtAddress { get; set; }
        public string? DoctorName { get; set; }
        public long? SalesId { get; set; }


    }
}