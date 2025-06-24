using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class ContantListDto
    {
        public long ConstantId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? ConstantType { get; set; }
    }
}
