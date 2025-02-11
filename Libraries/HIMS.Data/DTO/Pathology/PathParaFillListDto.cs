using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class PathParaFillListDto
    {
        public long ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string NormalRange { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public long IsNumeric { get; set; }

    }
}
