using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MLgdGovernmentDatum
    {
        public long Lgdid { get; set; }
        public long? StateCode { get; set; }
        public string? StateName { get; set; }
        public long? DistrictLgdcode { get; set; }
        public string? DistrictName { get; set; }
        public string? DistrictNameLocal { get; set; }
        public string? Hierarchy { get; set; }
        public string? ShortNameOfDistrict { get; set; }
        public long? Census2001Code { get; set; }
        public long? Census2011Code { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
