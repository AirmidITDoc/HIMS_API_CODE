using HIMS.Data.Models;
using LinqToDB;

namespace HIMS.Services.Administration
{
    public class BarcodeConfigService : IBarcodeConfigService
    {
        private readonly HIMSDbContext _context;
        public BarcodeConfigService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<BarcodeConfigMaster> GetConfigByCode(string TemplateCode)
        {
            return await _context.BarcodeConfigMasters.FirstOrDefaultAsync(x => x.TemplateCode == TemplateCode);
        }
    }
}
