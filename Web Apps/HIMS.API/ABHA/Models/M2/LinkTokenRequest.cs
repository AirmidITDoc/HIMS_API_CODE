namespace HIMS.API.ABHA.Models.M2
{
    public class LinkTokenRequest
    {
        public long AbhaNumber { get; set; }
        public string AbhaAddress { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int YearOfBirth { get; set; }
    }
}
