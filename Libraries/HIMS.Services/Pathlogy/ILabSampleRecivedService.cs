using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface ILabSampleRecivedService
    {
        Task Update(List<TPathologyReportHeader> ObjTPathologyReportHeader, int currentUserId, string CurrentUserName);
        Task<IPagedList<LabSampleRecivedListDto>> LabGetListAsync(GridRequestModel objGrid);


    }
}
