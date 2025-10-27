using System.ComponentModel.DataAnnotations.Schema;

namespace HIMS.Data.DTO.Master
{

    public partial class MCityMaster
    {
        [NotMapped]
        public string StateName { get; set; }
    }
}
