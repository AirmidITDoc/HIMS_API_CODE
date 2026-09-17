using System;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Services.DietKitchen;

namespace HIMS.Services.DietKitchen
{
    public partial interface IDietMenuMasterService
    {
        Task InsertAsync(MDietMenuMaster ObjMDietMenuMaster, int UserId, string Username);

    }
}
