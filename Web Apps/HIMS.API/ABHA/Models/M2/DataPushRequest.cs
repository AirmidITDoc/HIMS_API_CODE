namespace HIMS.API.ABHA.Models.M2
{
    public class DataPushRequest
    {
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public List<DataEntry> Entries { get; set; } = new List<DataEntry>();
        public KeyMaterial KeyMaterial { get; set; } = new KeyMaterial();
    }

    public class DataEntry
    {
        public string Content { get; set; } = string.Empty;
        public string Media { get; set; } = string.Empty;
        public string Checksum { get; set; } = string.Empty;
        public string CareContextReference { get; set; } = string.Empty;
    }

    public class KeyMaterial
    {
        public string CryptoAlg { get; set; } = string.Empty;
        public string Curve { get; set; } = string.Empty;
        public DhPublicKey DhPublicKey { get; set; } = new DhPublicKey();
        public string Nonce { get; set; } = string.Empty;
    }

    public class DhPublicKey
    {
        public string Expiry { get; set; } = string.Empty;
        public string Parameters { get; set; } = string.Empty;
        public string KeyValue { get; set; } = string.Empty;
    }
}
