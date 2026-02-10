using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.ScheduleJobs.Data
{
    public class SmsResponse
    {
        [JsonProperty("Job Id")]
        public int JobId { get; set; }
        public string Ack { get; set; }
        public long mobileNo { get; set; }
    }
}
