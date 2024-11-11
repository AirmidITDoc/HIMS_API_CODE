using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.Models
{
    public partial class DbPrefixMaster
    {
        [NotMapped]
        public string GenderName {  get; set; }
    }
}
