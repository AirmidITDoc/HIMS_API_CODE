using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.DietKitchen
{
    public partial interface IMealtypemasterService
    {
        Task InsertAsync(MMealTypeMaster ObjMMealTypeMaster, int UserId, string Username);

    }
}