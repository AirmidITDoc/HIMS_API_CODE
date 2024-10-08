﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Bedmaster
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
    }
}
