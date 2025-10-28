using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IReportConfigService
    {
        Task InsertAsyncm(MReportConfig ObjMReportConfig, int UserId, string Username);
        Task UpdateAsyncm(MReportConfig ObjMReportConfig, int UserId, string Username);
        Task<IPagedList<MReportConfigListDto>> MReportConfigList(GridRequestModel objGrid);


    }
}
