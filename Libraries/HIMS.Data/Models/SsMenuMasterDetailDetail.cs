using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsMenuMasterDetailDetail
    {
        public decimal MenuMasterDetailDetailMasterId { get; set; }
        public decimal MenuMasterDetailDetailMasterSrNo { get; set; }
        public decimal MenuMasterDetailDetailSrNo { get; set; }
        public string MenuMasterDetailDetailLinkName { get; set; } = null!;
        public string MenuMasterDetailDetailAction { get; set; } = null!;
        public decimal MenuMasterDetailDetailBlock { get; set; }
        public decimal MenuMasterDetailDetailDisplaySrNo { get; set; }
        public int MenuSubSubId { get; set; }
        public string? MenuMasterDetailDetailIcon { get; set; }
    }
}
