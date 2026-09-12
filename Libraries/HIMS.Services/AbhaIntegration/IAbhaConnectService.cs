using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;
namespace HIMS.Services.AbhaIntegration
{
    public interface IAbhaConnectService
    {
        Task<object> InitiateClient(object model);
        Task InsertAsync(TAbhaCallbackformation obj);
    }
}
