using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public class PatientvitalsdetailListDto
    {
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Bmi { get; set; }
        public string? CreatedDate { get; set; }
    }
}
