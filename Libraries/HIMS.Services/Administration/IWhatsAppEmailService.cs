using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using Microsoft.Extensions.Configuration;

namespace HIMS.Services.Administration
{
    public partial interface IWhatsAppEmailService
    {
        Task InsertAsync(TWhatsAppSmsOutgoing ObjWhatsApp, long Id, int UserId, string Username); //ReportRequestModel model, 
        Task InsertEmailAsync(TMailOutgoing ObjEmail, long Id, int UserId, string Username); //ReportRequestModel model, 
        Task InsertMailVimal(TMailOutgoing ObjEmail);
    }
}
