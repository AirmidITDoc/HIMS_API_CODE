using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class THomeDeliveryOrder
    {
        public long OrderId { get; set; }
        public string? OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderTime { get; set; }
        public long CustomerId { get; set; }
        public string? UploadDocument { get; set; }
        public long? StoreId { get; set; }
    }
}
