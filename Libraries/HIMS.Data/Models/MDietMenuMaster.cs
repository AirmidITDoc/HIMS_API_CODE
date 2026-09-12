using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDietMenuMaster
    {
        public MDietMenuMaster()
        {
            MDietMenuDetailMasters = new HashSet<MDietMenuDetailMaster>();
        }

        public long DietMenuId { get; set; }
        public string DietMenuCode { get; set; } = null!;
        public string DietMenuName { get; set; } = null!;
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public string? Texture { get; set; }
        public string? Calories { get; set; }
        public string? Protein { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MDietMenuDetailMaster> MDietMenuDetailMasters { get; set; }
    }
}
