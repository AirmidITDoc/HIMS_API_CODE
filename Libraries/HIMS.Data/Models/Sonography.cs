using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Sonography
    {
        public long SonographyId { get; set; }
        public long? OpdIpdId { get; set; }
        public DateTime? TranDate { get; set; }
        public string? Liver { get; set; }
        public string? Spleen { get; set; }
        public string? Pancrea { get; set; }
        public string? RtKidney { get; set; }
        public string? LtKideny { get; set; }
        public string? Uterus { get; set; }
        public string? RtOvary { get; set; }
        public string? LtOvary { get; set; }
        public string? Comment { get; set; }
    }
}
