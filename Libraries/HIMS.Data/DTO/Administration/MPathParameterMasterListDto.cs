using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class MPathParameterMasterListDto
    {
        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
        public string? ParameterName { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public long? IsNumeric { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsPrintDisSummary { get; set; }
        public string? UnitName { get; set; }
        public string? Formula { get; set; }
        public long? Isdeleted { get; set; }
        public string? IsBoldFlag { get; set; }
        public string? MethodName { get; set; }
    }
}
