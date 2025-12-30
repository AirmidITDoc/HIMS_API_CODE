using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IAdminTaskService
    {
        Task BilldateUpdateAsync(Bill ObjBill, int CurrentUserId, string CurrentUserName);
        Task Update(Admission ObjAdmission, int UserId, string Username);



    }
}
