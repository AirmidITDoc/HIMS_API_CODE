using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class ModuleWiseConfigListDto
    {
        public int SmsConfigId { get; set; }
        public int MenuId { get; set; }
        public string? ModuleName { get; set; } 
        public bool IsWhatsApp { get; set; }
        public bool IsEmail { get; set; }
        public bool IsSms { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string? ModifiedDate { get; set; }
    }
}
