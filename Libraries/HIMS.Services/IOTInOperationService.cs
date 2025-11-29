using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services
{
    public partial interface IOTInOperationService
    { 
         Task InsertAsync(TOtInOperationHeader ObjTOtInOperationHeader, int UserId, string Username);
        Task UpdateAsync(TOtInOperationHeader ObjTOtInOperationHeader, int UserId, string Username, string[]? references);

    }
}
