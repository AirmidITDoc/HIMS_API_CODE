using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class IpdDrugScheduleDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public int Status { get; set; }
        public int DoseNo { get; set; }
        public string FirstName { get; set; }
        public string Comment { get; set; }
        public string Route { get; set; }
        public string Freq { get; set; }
        public string DoseName { get; set; }
        public string DrugName { get; set; }
        //public float DoseQtyPerDay { get; set; }
       // public long RegId { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string City { get; set; }
        public string Age { get; set; }
    }
}
