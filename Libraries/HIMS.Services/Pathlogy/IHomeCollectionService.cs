using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IHomeCollectionService
    {
        Task InsertAsync(THomeCollectionRegistrationInfo ObjTHomeCollectionRegistrationInfo, int UserId, string Username);

    }
}
