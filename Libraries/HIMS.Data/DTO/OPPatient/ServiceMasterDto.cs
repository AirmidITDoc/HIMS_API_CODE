using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    //public partial class ServiceMasterList
    //{
    //    public decimal? ClassRate { get; set; }
    //    public long? TariffId { get; set; }
    //    public long? ClassId { get; set; }
    //}

    public class ServiceMasterDTO : ServiceMaster
    {
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal? ClassRate { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public string FormattedText { get { return this.ServiceName + " | " + this.ClassRate; } }
    }
}
