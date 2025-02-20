using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class MPathParameterListDto
    {
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public long ParameterId { get; set; }
        public string ParameterShortName { get; set; }
        public string ParameterName { get; set; }
        public string PrintParameterName { get; set; }
        public string Formula { get; set; }
        public long IsNumericParameter { get; set; }
        public bool Isdeleted { get; set; }
        public long AddedBy { get; set; }
        public string IsBoldFlag { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsPrintDisSummary { get; set; }
        public string MethodName { get; set; }
       
       
      

 

   
       
    }
}
