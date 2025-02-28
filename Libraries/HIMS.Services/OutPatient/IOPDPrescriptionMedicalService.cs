using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial  interface IOPDPrescriptionMedicalService
    {
    
        Task InsertPrescriptionAsyncSP(TPrescription objTPrescription,List<TOprequestList> objTOprequestList ,List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosisMaster, int UserId, string UserName);


    }
}
