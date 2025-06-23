using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class ConsentDeptListDto
    {
        public long ConsentId { get; set; }
        public string ConsentName { get; set; }
        public string ConsentDesc { get; set; }
    }
}
