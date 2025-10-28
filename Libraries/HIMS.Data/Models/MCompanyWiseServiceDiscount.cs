using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCompanyWiseServiceDiscount
    {
        public long CompServiceDetailId { get; set; }
        public bool? IsGroupOrSubGroup { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public double? DiscountPercentage { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
