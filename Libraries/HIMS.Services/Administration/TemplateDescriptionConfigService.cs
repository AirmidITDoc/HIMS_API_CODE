using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace HIMS.Services.Administration
{
    public  class TemplateDescriptionConfigService:ITemplateDescriptionConfigService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public TemplateDescriptionConfigService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<List<GetDischargeTemplateListDto>> GetDischargeTemplateListAsync(string CategoryName)
        {
            var query = _context.MReportTemplateConfigs.AsQueryable();

            if (!string.IsNullOrEmpty(CategoryName))
            {
                string lowered = CategoryName.ToLower();
                query = query.Where(d => d.CategoryName != null && d.CategoryName.ToLower().Contains(lowered));
            }

            var data = await query
                .OrderBy(d => d.TemplateId)
                .Select(d => new GetDischargeTemplateListDto
                {
                    TemplateId = d.TemplateId,
                    CategoryName = d.CategoryName,
                    TemplateName = d.TemplateName,
                    TemplateHeader = d.TemplateHeader,
                    TemplateDescription = d.TemplateDescription

                })
                .Take(50)
                .ToListAsync();

            return data;
        }

    }
}
