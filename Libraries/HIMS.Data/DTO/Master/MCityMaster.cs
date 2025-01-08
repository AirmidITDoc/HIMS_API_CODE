using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Master
{
   
    public partial class MCityMaster
    {
        [NotMapped]
        public string StateName { get; set; }
    }
}
