using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IPathologyTemplateService
    {
        Task InsertAsyncSP(MTemplateMaster objTemplate, int UserId, string Username);

    }
}
