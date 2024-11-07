using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
   public partial interface IDoctorShareMasterService
    {
        Task InsertAsync(MDoctorPerMaster objMDoctorPerMaster, int UserId, string Username);
       
        Task UpdateAsync(MDoctorPerMaster objMDoctorPerMaster, int UserId, string Username);
    }
}
