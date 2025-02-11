using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class PathSubtestFillListDto
    {
        public long ParameterId { get; set; }
        public string ParameterName { get; set; }
        public long TestId { get; set; }
        public string TestName { get; set; }

    }
}
