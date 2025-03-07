using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class TestMasterListDto
    {

        public long? SubTestID { get; set; }
        public long? ParameterID { get; set; }
        public string? ParameterName { get; set; }
        public bool? IsSubTest { get; set; }
        public long? TestId { get; set; }
        public bool? IsActive { get; set; }
      
    }
}
