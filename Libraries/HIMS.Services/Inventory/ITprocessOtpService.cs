using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial  interface ITprocessOtpService
    {
        //Task UpdateAsync(TProcessOtp objPurchase, int UserId, string Username, string[]? references);
        Task UpdateAsync(TProcessOtp ObjTProcessOtp, int CurrentUserId, string CurrentUserName);
    }
}
