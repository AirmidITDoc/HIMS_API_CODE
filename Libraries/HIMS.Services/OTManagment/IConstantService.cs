using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public partial interface IConstantService
    {
        Task<IPagedList<ConstantsListDto>> GetListAsync(GridRequestModel objGrid);
        List<SearchConstantsDto> SearchConstants(string Keyword);
        Task Update(MConstant ObjMConstant, int currentUserId, string CurrentUserName);

    }
}
    