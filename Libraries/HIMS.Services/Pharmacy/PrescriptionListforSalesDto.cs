using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class PrescriptionListforSalesDto
    {
        public long IPPreId { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? VisitDate { get; set; }
        public string? DoctorName { get; set; }
        public string? Date { get; set; }
        public string? PatientType { get; set; }
        public long? OP_IP_ID { get; set; }
        public bool? IsClosed { get; set; }
        public string? PTime { get; set; }
        public long? RegId { get; set; }
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public long? IPMedID { get; set; }
        public long? WardId { get; set; }
        public long? bedId { get; set; }
        public long? PresWardID { get; set; }
        public string? IPDNo { get; set; }
        public string? RoomName { get; set; }



    }
}
