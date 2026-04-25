using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class DoseMasterListDto
    {
        public long DoseId { get; set; }
        public string? DoseName { get; set; }
        public string? DoseNameInEnglish { get; set; }
        public string? DoseNameInMarathi { get; set; }
        public bool? IsActive { get; set; }
        public double? DoseQtyPerDay { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
