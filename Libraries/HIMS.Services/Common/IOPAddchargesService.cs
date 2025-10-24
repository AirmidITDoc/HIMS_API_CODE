using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Common
{
    public partial interface IOPAddchargesService
    {
        void InsertSP(AddCharge objAddcharges, int currentUserId, string currentUserName);
        void DeleteAsyncSP(AddCharge objAddcharges, int currentUserId, string currentUserName);
    }
}
