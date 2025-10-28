using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class UserChatMailSystem
    {
        public string? UserSend { get; set; }
        public string? UserReceive { get; set; }
        public string? Mgs { get; set; }
        public DateTime? Xdate { get; set; }
        public DateTime? Xtime { get; set; }
        public long? LoopId { get; set; }
        public string? Xdefine { get; set; }
    }
}
