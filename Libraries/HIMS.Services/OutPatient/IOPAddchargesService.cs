using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPAddchargesService
    {
        Task InsertAsyncSP(AddCharge objAddcharges, int currentUserId, string currentUserName);

    }
}
