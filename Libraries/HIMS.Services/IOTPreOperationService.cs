using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services
{
    public partial interface IOTPreOperationService
    {
        Task InsertAsync(TOtPreOperationHeader ObjTOtPreOperationHeader, int UserId, string Username);

    }
}
