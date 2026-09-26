using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.MRD;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public partial interface IMrdDiagnosisInfoService
    {
        Task<IPagedList<MrdDiagnosisInfoListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsync(TIpMrdDiagnosisInfoHeader ObjHeader, List<TIpMrdDiagnosisInfoDetail> ObjDetailList, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(TIpMrdDiagnosisInfoHeader ObjHeader, List<TIpMrdDiagnosisInfoDetail> ObjDetailList, int CurrentUserId, string CurrentUserName);
    }
}
