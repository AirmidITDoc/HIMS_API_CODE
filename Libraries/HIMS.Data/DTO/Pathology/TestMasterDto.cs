using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public  class TestMasterDto
    {
        public  long SubTestID { get; set; }
        public long ParameterID { get; set; }
        public string? ParameterName { get; set; }
        public string? IsSubTest { get; set; }
        public long? TestId { get; set; }

    }
}
