using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IHelpdeskPatientComplaintService
    {
        Task<IPagedList<ComplaintListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
