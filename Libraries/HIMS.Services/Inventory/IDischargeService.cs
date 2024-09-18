using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IDischargeService
    {
        Task InsertAsync(Discharge objDischarge, int UserId, string Username);
        Task UpdateAsync(Discharge objDischarge, int UserId, string Username);
    }
}
