using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ModuleMaster
    {
        public ModuleMaster()
        {
            PageMasters = new HashSet<PageMaster>();
        }

        public int Id { get; set; }
        public string ModuleName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<PageMaster> PageMasters { get; set; }
    }
}
