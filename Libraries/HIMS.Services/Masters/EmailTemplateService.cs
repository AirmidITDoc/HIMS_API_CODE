using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.Data.SqlClient;

namespace HIMS.Services.Masters
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public EmailTemplateService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<EmailTemplateMaster> GetTemplateByCode(string TemplateCode)
        {
            return await _context.EmailTemplateMasters.FirstOrDefaultAsync(x => x.TemplateCode == TemplateCode);
        }
    }
}
