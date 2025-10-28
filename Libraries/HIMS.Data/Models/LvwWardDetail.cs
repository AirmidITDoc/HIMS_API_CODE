using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwWardDetail
    {
        public long RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? RoomType { get; set; }
        public long? LocationId { get; set; }
        public string? LocationName { get; set; }
        public bool? IsAvailible { get; set; }
        public bool? IsActive { get; set; }
        public long? ClassId { get; set; }
        public string? ClassName { get; set; }
    }
}
