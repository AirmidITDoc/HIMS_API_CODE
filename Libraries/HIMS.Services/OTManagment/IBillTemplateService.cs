using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OTManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public partial interface IBillTemplateService
    {
        Task<IPagedList<OtBillTemplateListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
