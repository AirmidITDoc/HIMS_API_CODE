using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.Models
{
    public partial class DoctorMaster
    {
        [NotMapped]
        public string PrefixName { get; set; }
        [NotMapped]
        public string DoctorTypeName { get; set; }
        [NotMapped]
        public string DeptNames { get; set; }
    }
}