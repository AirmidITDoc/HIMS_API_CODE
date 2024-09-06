using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IItemMasterService
    {
        Task InsertAsync(MItemMaster objItem, int UserId, string Username);
        Task InsertAsyncSP(MItemMaster objItem, int UserId, string Username);
    }
}
