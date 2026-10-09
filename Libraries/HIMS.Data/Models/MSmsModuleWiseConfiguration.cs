using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSmsModuleWiseConfiguration
    {
        public int SmsConfigId { get; set; }
        public int MenuId { get; set; }
        public string ModuleName { get; set; } = null!;
        public bool IsWhatsApp { get; set; }
        public string? WhatsAppFormat { get; set; }
        public bool IsEmail { get; set; }
        public string? EmailFormat { get; set; }
        public bool IsSms { get; set; }
        public string? SmsFormat { get; set; }
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual MenuMaster Menu { get; set; } = null!;
    }
}
