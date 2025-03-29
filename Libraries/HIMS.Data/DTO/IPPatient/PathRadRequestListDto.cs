using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PathRadRequestListDto
    {
        public long? ReqDetId { get; set; }
        public string? ServiceName { get; set; }
        public long? ServiceId { get; set; }
        public string? opipid { get; set; }
        public string? opiptype { get; set; }
        public bool? IsStatus { get; set; }
        public long? IsPathology { get; set; }
        public long IsRadiology { get; set; }
        public string? ReqDate { get; set; }
        public string? ReqTime { get; set; }
        public string? BillingUser { get; set; }
        public string? AddedByDate { get; set; }
        public decimal? price { get; set; }
    }

}
