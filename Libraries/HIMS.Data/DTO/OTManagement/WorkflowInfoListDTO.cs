using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public class WorkflowInfoListDTO
    {

        public long WorkflowId { get; set; }
        public string WorkFlowName { get; set; } = null!;
        public string Wfurl { get; set; } = null!;
        public string Wfmode { get; set; } = null!;
        public bool Active { get; set; }
    }
}

