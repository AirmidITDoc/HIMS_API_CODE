using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IWhatsAppEmailService
    {
        Task InsertAsync(TWhatsAppSmsOutgoing ObjWhatsApp, int UserId, string Username); //ReportRequestModel model, 

    }
}
