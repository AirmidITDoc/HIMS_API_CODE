using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class BedTransferDetailListDto
    {
        public string RegNo {  get; set; }
        public string PatientName { get; set; }
        public string AdmissionDate { get; set; }
        public string AdmissionTime { get; set; }
        public string IPDNo { get; set; }
        public string FromWardName { get; set; }
        public string FromBedNo { get; set; }
        public string FromClassName { get; set; }
        public string ToWardName { get; set; }
        public string ToBedNo { get; set; }
        public string ToClassName { get; set; }
        public string OrderNo { get; set; }
        public long TransferId { get; set; }



    }
}
