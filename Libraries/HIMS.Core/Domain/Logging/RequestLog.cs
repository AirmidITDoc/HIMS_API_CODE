using System;

namespace HIMS.Core.Domain.Logging
{
    public class RequestLog 
    {
        public RequestLog()
        {
            Path = string.Empty;
            QueryString = string.Empty;
            Method = string.Empty;
            Payload = string.Empty;
            Response = string.Empty;
            ResponseCode = string.Empty;
            RequestedOn = DateTime.UtcNow;
            RespondedOn = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Method { get; set; }
        public string Payload { get; set; }
        public string Response { get; set; }
        public string ResponseCode { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime RespondedOn { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}