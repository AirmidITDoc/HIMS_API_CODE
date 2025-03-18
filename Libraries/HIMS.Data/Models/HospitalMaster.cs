using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class HospitalMaster
    {
        public long HospitalId { get; set; }
        public string? HospitalHeaderLine { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? EmailId { get; set; }
        public string? WebSiteInfo { get; set; }
        public string? Header { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
