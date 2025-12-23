using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Master
{
    public class BedmasterListDto
    {
        public long BedId { get; set; }
        public string? BedName { get; set; }
        public long? RoomId { get; set; }
        public bool? IsAvailible { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? RoomName { get; set; }

    }
}
