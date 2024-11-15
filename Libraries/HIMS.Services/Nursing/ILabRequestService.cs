using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial  interface ILabRequestService
    {
        Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username);
        Task<IPagedList<LabRequestListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<LabRequestDetailsListDto>> GetListAsyncD(GridRequestModel objGrid);




    }
}
