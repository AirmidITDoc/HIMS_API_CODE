using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DistrictLGDCodeDto
    {
        public long CityId { get; set; }
        public string? DistrictLgdcode { get; set; }
        public string? CityName { get; set; }
        public string? StateCode { get; set; }
        public long? StateId { get; set; }
        public bool? IsActive { get; set; }
        public string? StateName { get; set; }
    }
}
