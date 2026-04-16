using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.NewPharmacy
{
    public class PrescriptionReturnHListDto
    {

        public long PresReId { get; set; }
        public string? PresNo { get; set; }
        public string? PresDate { get; set; }
        public string? PresTime { get; set; }
        public long? ToStoreId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? Addedby { get; set; }
        public bool? IsActive { get; set; }
        public bool? Isclosed { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? BedName { get; set; }
        public string? RoomName { get; set; }
        public string? IPDNo { get; set; }
        public string? AdmissionDate { get; set; }
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? TariffName { get; set; }
        public string? Age { get; set; }
        public string? GenderName { get; set; }
        public long DoctorId { get; set; }
        public string? DoctorName { get; set; }
    }
      
    public class PrescriptionReturnDetailsListDto
    {

        public long PresDetailsId { get; set; }
        public long? PresReId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? Qty { get; set; }
        public bool? IsClosed { get; set; }
        public string? ItemName { get; set; }

        

    }
}

