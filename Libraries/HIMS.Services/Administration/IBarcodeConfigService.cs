using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IBarcodeConfigService
    {
        Task<BarcodeConfigMaster> GetConfigByCode(string TemplateCode);
    }
}
