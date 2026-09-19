using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.MRD
{
    public interface I_ICDUpdateService
    {
        Task InsertICDSp(TPatIcdcdeH ObjTPatIcdcdeH, List<TPatIcdcdeD> ObjTPatIcdcdeDList, int CurrentUserId, string CurrentUserName);
    }
}
