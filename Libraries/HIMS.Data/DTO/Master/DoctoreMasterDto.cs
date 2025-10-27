using System.ComponentModel.DataAnnotations.Schema;

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