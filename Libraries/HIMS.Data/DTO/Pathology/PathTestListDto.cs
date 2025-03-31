using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public  class PathTestListDto
    {
        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long CategoryId { get; set; }
        public bool? IsSubTest { get; set; }
        public string? TechniqueName { get; set; }
        public string? MachineName { get; set; }
        public string? SuggestionNote { get; set; }
        public string FootNote { get; set; }
        public long AddedBy { get; set; }
        public string? CategoryName { get; set; }
        public long ServiceID { get; set; }
        public string? IsTemplateTest { get; set; }
        public string? ServiceName { get; set; }
        public bool IsActive { get; set; }

    }
}
