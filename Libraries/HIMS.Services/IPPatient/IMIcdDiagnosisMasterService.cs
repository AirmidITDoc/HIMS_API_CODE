using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial interface IMIcdDiagnosisMasterService
    {
        List<MIcdDiagnosisMasterDto> GetMIcdDiagnosis(string Keyword);
    }

}
