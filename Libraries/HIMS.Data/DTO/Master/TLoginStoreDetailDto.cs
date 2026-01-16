using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.Models
{
    public partial class TLoginStoreDetail
    {
        [NotMapped]
        public string StoreName { get; set; }
    }
    public partial class TLoginUnitDetail
    {
        [NotMapped]
        public string UnitName { get; set; }
    }
}
