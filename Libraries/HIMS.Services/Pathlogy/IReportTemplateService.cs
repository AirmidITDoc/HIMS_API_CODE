using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;

namespace HIMS.Services.Pathlogy
{
    public partial interface IReportTemplateService
    {
        Task<IPagedList<ReportTemplateListDto>> ReportTemplateList(GridRequestModel objGrid);
    }
}
