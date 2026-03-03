using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.MRD
{
    public  class MRDFileReceivedListDto
    {

        public long RMDRecordId { get; set; }
        public DateTime RecievedDate { get; set; }
        public DateTime? RecievedTime { get; set; }
        public long UnitId { get; set; }
        public long Opipid { get; set; }
        public string Mrdno { get; set; } = null!;
        public string? Location { get; set; }
        public string? Comments { get; set; }
        public bool IsInOut { get; set; }
        public long? OutFileId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long GivenUserId { get; set; }
        public string? PersonName { get; set; }
        public DateTime OutDate { get; set; }
        public DateTime OutTime { get; set; }
        public string? OutReason { get; set; }
        public string? InNo { get; set; }
        public DateTime? InDate { get; set; }
        public DateTime? InTime { get; set; }
        public long? ReturnUserId { get; set; }
        public string? ReturnPersonName { get; set; }
        public string? InReason { get; set; }



        public string? PatientName { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public DateTime? AdmissionTime { get; set; }
        //public string? PatientType { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }

        public string? IPDNo { get; set; }
        public string? TariffName { get; set; }

        public string? DoctorName { get; set; }

        public string? InFileInfo { get; set; }
        public string? OutFileInfo { get; set; }

        

        

    }
}
