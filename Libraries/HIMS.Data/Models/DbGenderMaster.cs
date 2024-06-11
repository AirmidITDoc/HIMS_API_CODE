using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DbGenderMaster
    {
        public long GenderId { get; set; }
        public string? GenderName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
