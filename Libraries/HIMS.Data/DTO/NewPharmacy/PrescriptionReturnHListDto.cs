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
        public DateTime? PresDate { get; set; }
        public DateTime? PresTime { get; set; }
        public long? ToStoreId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? Addedby { get; set; }
        public bool? IsActive { get; set; }
        public bool? Isclosed { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RegNo { get; set; }

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

