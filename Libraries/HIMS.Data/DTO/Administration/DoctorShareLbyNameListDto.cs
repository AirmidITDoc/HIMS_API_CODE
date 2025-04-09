using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DoctorShareLbyNameListDto
    {

        public long? DoctorShareId { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? ServicePercentage { get; set; }
        public long? ClassId { get; set; }
        public string? ClassName { get; set; }
        public byte? DocShrType { get; set; }
        public string? DocShrTypeS { get; set; }
        public decimal? ServiceAmount { get; set; }
        public long? ShrTypeSerOrGrp { get; set; }
        public byte? Op_IP_Type { get; set; }
    }
}
