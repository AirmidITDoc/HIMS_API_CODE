using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory
{
    public class MaterialConsumptionHeaderModel
    {
        public long MaterialConsumptionId { get; set; }
        public string? ConsumptionNo { get; set; }
        public DateTime? ConsumptionDate { get; set; }
        public string? ConsumptionTime { get; set; }
        public long? FromStoreId { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? AdmId { get; set; }
    }
}