using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class MPathParaRangeWithAgeMasterListDto
    {
        public long PathparaRangeId { get; set; }
        public long? ParaId { get; set; }
        public long? SexId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? AgeType { get; set; }
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
    }
}
