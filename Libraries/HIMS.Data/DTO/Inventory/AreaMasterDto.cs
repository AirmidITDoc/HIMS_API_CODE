using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class AreaMasterDto
    {

            public long AreaId { get; set; }
            public string? AreaName { get; set; }
            public long CityId { get; set; }
            public string? CityName { get; set; }
            public string? Pincode { get; set; }
            public string? Area { get; set; }
    }
}

