using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsRoleTemplateMaster
    {
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? AddedByDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedByDate { get; set; }
    }
}
