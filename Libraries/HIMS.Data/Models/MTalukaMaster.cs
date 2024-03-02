using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MTalukaMaster
    {
        public long TalukaId { get; set; }
        public string? TalukaName { get; set; }
        public long? CityId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
