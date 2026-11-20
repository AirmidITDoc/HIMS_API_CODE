using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class HimsLoginChallenge
    {
        public string ChallengeId { get; set; } = null!;
        public string Kid { get; set; } = null!;
        public string ContextValue { get; set; } = null!;
        public string Nonce { get; set; } = null!;
        public int LoginType { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public byte[] CaptchaHash { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; }
    }
}
