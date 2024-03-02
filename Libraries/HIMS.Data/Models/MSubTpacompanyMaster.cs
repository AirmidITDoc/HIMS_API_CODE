using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSubTpacompanyMaster
    {
        public long SubCompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? FaxNo { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
    }
}
