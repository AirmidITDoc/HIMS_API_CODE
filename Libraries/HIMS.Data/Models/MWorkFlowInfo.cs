using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MWorkFlowInfo
    {
        public long WorkflowId { get; set; }
        public string WorkFlowName { get; set; } = null!;
        public string Wfurl { get; set; } = null!;
        public string Wfmode { get; set; } = null!;
        public bool Active { get; set; }
    }
}
