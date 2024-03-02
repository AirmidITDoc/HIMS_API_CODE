using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsMenuMaster
    {
        public decimal MenuMasterId { get; set; }
        public string MenuMasterLinkName { get; set; } = null!;
        public string MenuMasterIcon { get; set; } = null!;
        public string MenuMasterController { get; set; } = null!;
        public string MenuMasterAction { get; set; } = null!;
        public decimal MenuMasterDisplaySrNo { get; set; }
        public decimal MenuMasterBlock { get; set; }
        public int Id { get; set; }
    }
}
