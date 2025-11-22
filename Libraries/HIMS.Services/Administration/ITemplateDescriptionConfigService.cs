using HIMS.Data.DTO.OPPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface ITemplateDescriptionConfigService
    {
        Task<List<GetDischargeTemplateListDto>> GetDischargeTemplateListAsync(string CategoryName);

    }
}
