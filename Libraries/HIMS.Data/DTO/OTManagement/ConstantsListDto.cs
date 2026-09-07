using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public  class ConstantsListDto
    {
        public long ConstantId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? ConstantType { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public class SearchConstantsDto
    {
        
        public string? ConstantType { get; set; }
       
    }
}
