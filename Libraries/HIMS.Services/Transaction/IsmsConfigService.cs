using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Transaction
{
    public partial  interface IsmsConfigService
    {
        Task InsertAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username);
        Task UpdateAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username);
    }
}
