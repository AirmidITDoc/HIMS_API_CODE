using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public partial  interface IMenuMasterService
    {
        Task InsertAsync(MenuMaster objMenuMaster, int UserId, string Username);
        Task UpdateAsync(MenuMaster objMenuMaster, int UserId, string Username);
    }
}
