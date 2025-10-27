using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;

namespace HIMS.Services.Pathlogy
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ReportTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ReportTemplateListDto>> ReportTemplateList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ReportTemplateListDto>(model, "m_Rtrv_ReportTemplateConfig");
        }
    }
}
