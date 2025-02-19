using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Data.DTO.Administration
{
    public partial class MParameterDescriptiveMasterListDto
    {
        public long DescriptiveId { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterValues { get; set; }
        public bool? IsDefaultValue { get; set; }
       
    }
}
