using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsRoleTemplateDetail
    {
        public long RoleDetId { get; set; }
        public long? RoleId { get; set; }
        public long? MenuMasterId { get; set; }
        public string? MenuMasterIcon { get; set; }
        public string? MenuMasterLinkName { get; set; }
        public string? MenuMasterAction { get; set; }
        public string? MenuMasterDetailLinkName { get; set; }
        public string? MenuMasterDetailAction { get; set; }
        public string? MenuMasterDetailDetailLinkName { get; set; }
        public string? MenuMasterDetailDetailAction { get; set; }
        public string? MenuMasterDetailIcon { get; set; }
        public string? MenuMasterDetailDetailIcon { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? AddedByDate { get; set; }
    }
}
