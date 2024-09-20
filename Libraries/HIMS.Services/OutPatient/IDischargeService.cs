using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
   
        public partial interface IDischargeService
        {
            Task InsertAsyncSP(Discharge objDischarge, Admission objAdmission, int currentUserId, string currentUserName);
        Task UpdateAsyncSP(Discharge objDischarge, Admission objAdmission, int currentUserId, string currentUserName);
    }
    }
