using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public partial interface ISalesService
    {
        Task InsertAsync(TSalesHeader user, int UserId, string Username);
    }
}
