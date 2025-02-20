using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class RadiologyTestListDto
    {
        public long? TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
    }
}
