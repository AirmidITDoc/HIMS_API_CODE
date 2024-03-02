using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPackageDetail
    {
        public long PackageId { get; set; }
        public long? ServiceId { get; set; }
        public long? PackageServiceId { get; set; }
        public decimal? Price { get; set; }
    }
}
