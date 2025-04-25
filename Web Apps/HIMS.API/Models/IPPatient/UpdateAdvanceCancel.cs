using HIMS.Data.Models;

namespace HIMS.API.Models.IPPatient
{
    public class UpdateAdvanceCancel
    {
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long AdvanceDetailId { get; set; }

    }
    public class UpdateRefundModel
    {
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public long RefundId { get; set; }


    }
    public class PharmSalesPaymentModel
    {
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long SalesId { get; set; }

    }
}
