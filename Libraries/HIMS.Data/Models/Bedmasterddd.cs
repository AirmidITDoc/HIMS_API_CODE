using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Bedmasterddd
    {
        public long BedId { get; set; }
        public string? BedName { get; set; }
        public long? RoomId { get; set; }
        public bool? IsAvailible { get; set; }
        public bool? IsActive { get; set; }
    }
}
