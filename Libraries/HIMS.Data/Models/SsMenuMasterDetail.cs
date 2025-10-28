using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsMenuMasterDetail
    {
        public decimal MenuMasterDetailMasterId { get; set; }
        public decimal MenuMasterDetailSrNo { get; set; }
        public decimal MenuMasterDetailDisplaySrNo { get; set; }
        public decimal MenuMasterDetailBlock { get; set; }
        public string MenuMasterDetailLinkName { get; set; } = null!;
        public string MenuMasterDetailAction { get; set; } = null!;
        public int MenuSubId { get; set; }
        public string? MenuMasterDetailIcon { get; set; }
    }
}
