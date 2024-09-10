using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface IRadiologyTestService
    {
        Task InsertAsyncSP(MRadiologyTestMaster objRadio, int UserId, string Username);
        Task InsertAsync(MRadiologyTestMaster objRadio, int UserId, string Username);
    }
}
