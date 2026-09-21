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
    //    Task UpdateAsync(MDietMenuMaster ObjMDietMenuMaster, int UserId, string Username);
        Task UpdateAsync(MDietMenuMaster model, int currentUserId, string currentUserName, string[]? ignoreColumns = null);
    //    Task UpdateAsync(MDietMenuMaster model, object currentUserId, object currentUserName, string[]? ignoreColumns = null));
        //  Task UpdateAsync(MDietMenuMaster model, object currentUserId, object currentUserName, string[] strings);
    }
}
