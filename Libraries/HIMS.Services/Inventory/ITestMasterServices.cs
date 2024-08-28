using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ITestMasterServices
    {

        Task InsertAsync(MPathTestMaster objTest, int UserId, string Username);
        Task InsertAsyncSP(MPathTestMaster objTest, int UserId, string Username);
    }
}
