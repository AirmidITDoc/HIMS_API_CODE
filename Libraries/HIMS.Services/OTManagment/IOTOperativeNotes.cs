using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public partial interface IOTOperativeNotes
    {
        Task InsertAsync(TOtOperativeNote ObjTOtOperativeNotes, int UserId, string Username);
    }
}
