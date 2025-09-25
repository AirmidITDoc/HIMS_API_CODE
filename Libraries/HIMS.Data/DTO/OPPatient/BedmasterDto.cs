using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class BedmasterDto
    {
        public long BedId { get; set; }
        public long? RoomId { get; set; }
        public string BedName { get; set; }
    }
}
