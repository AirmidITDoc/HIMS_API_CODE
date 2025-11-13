using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using Microsoft.Extensions.Configuration;

namespace HIMS.Services.Administration
{
    public partial interface IWhatsAppEmailService
    {
        Task InsertAsync(TWhatsAppSmsOutgoing ObjWhatsApp, IConfiguration _configuration, int Id, int UserId, string Username); //ReportRequestModel model, 

    }
}
