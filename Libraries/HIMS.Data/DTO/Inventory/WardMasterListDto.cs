using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class WardMasterListDto
    {
        public long RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? RoomType { get; set; }
        public bool? IsAvailible { get; set; }
        public bool? IsActive { get; set; }
        public string? LocationName { get; set; }
        public string? ClassName { get; set; }
        public long? LocationId { get; set; }
        public long? ClassID { get; set; }
        public int? OccuipedCount { get; set; }
        public int? AvailableCount { get; set; }


    }
}
