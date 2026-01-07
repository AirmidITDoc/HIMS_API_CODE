using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial  interface IGastrologyEMRService
    {
        Task<IPagedList<ClinicalQuesListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
