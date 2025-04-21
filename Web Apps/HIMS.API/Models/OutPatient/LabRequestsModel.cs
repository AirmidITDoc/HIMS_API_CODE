using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.OutPatient
{
    public class LabRequestsModel
    {

        public long? OpdIpdId { get; set; }
        public long ClassID { get; set; }
        public long? ServiceId { get; set; }
        public long? TraiffId { get; set; }
        public long? ReqDetId { get; set; }
        public long? UserId { get; set; }
        public DateTime? ChargesDate { get; set; }

    }
}