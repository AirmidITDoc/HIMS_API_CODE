using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public interface IMenuMasterService
    {
        Task InsertAsync(MenuMaster objMenuMaster, int UserId, string Username);

    }
}
