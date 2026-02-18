using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IEmailTemplateService
    {
        Task<EmailTemplateMaster> GetTemplateByCode(string TemplateCode);
    }
}
