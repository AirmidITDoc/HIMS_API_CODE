using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionListDto
    {
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string Vst_Adm_Date { get; set; }
        public string Date { get; set; }
        public long OP_IP_ID { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public string StoreName { get; set; }
        public long IPMedID { get; set; }
        public string CompanyName { get; set; }
        public long CompanyId { get; set; }
        public long IppreId { get; set; }
        public DateTime? Pdate { get; set; }
        public DateTime? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Remark { get; set; }
        public bool? IsClosed { get; set; }
        public long? IsAddBy { get; set; }
        public long? StoreId { get; set; }
        public long? WardId { get; set; }
        public string? WardName { get; set; }
        public string? ClassName { get; set; }
        public string? GenericName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Ipdno { get; set; }
        public string? PatientType { get; set; }
        public string? ConDoctorName { get; set; }




        
    }
}
