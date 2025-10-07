using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial  interface IDoctorShareProcessService
    {
        Task DoctorShareInsertAsync(AddCharge ObjAddCharge, int UserId, string Username, DateTime FromDate, DateTime ToDate);

    }
}
