using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class OPRequestListFromEMRDto
    {
        public long RequestTranId {  get; set; }
        public long OPIPID { get; set; }
        public string GroupName { get; set; }
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public long CreatedBy { get; set; }
        public string UserName { get; set; }


    }
}
