using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public class ConsentMasterDto
    {
        public long ConsentId { get; set; }
        public long? DepartmentId { get; set; }
        public string? ConsentType { get; set; }
        public string? ConsentName { get; set; }
        public string? ConsentDesc { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public string? Value { get; set; }
        public string? DepartmentName { get; set; }
    }
}
