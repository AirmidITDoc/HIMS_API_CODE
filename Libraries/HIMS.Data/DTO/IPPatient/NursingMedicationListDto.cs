using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class NursingMedicationListDto
    {
        public long MedChartId {  get; set; }
        public string  MDate { get; set; }
        public string MTime { get; set; }
        public string DurgId { get; set; }
        public string DrugName { get; set; }
        public string DoseName { get; set; }
        public string Route { get; set; }
        public string NurseName { get; set; }
        public string CreatedBy { get; set; }
        public string Freq { get; set; }
        public long DoseID { get; set; }



    }
}
