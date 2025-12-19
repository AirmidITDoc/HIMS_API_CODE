using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;

namespace HIMS.Services.Radiology
{
    public partial interface IRadilogyService
    {
        Task<IPagedList<RadiologyListDto>> GetListAsync(GridRequestModel objGrid);
        Task RadiologyUpdate(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string Username);
        Task UpdateAsync(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string Username);
        Task VerifyAsync(TRadiologyReportHeader ObjTRadiologyReportHeader, int UserId, string Username);


    }
}
