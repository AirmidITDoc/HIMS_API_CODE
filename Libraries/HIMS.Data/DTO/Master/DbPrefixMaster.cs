using System.ComponentModel.DataAnnotations.Schema;

namespace HIMS.Data.Models
{
    public partial class DbPrefixMaster
    {
        [NotMapped]
        public string GenderName { get; set; }
    }
}
