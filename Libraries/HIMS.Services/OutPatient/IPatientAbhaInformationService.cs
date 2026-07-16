using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial  interface IPatientAbhaInformationService
    {
        Task InsertAsync(TPatientAbhaInformation ObjTPatientAbhaInformation, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(TPatientAbhaInformation ObjTPatientAbhaInformation, int CurrentUserId, string CurrentUserName);

    }
}
