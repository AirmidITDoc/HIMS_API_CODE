using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public partial interface IDietPatientRequestService
    {
        Task InsertAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username);
        Task UpdateAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username, string[]? references);

    }
}
