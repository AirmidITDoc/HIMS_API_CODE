﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsSmsConfig
    {
        public string? Url { get; set; }
        public string? Keys { get; set; }
        public string? Campaign { get; set; }
        public int? Routeid { get; set; }
        public string? SenderId { get; set; }
        public string? UserName { get; set; }
        public string? Spassword { get; set; }
        public string? StorageLocLink { get; set; }
        public string? ConType { get; set; }
    }
}
