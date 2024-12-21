using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
