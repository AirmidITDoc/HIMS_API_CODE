using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial  interface ILabRequestService
    {
        Task InsertAsync(THlabRequest objTHlabRequest, int UserId, string Username);
        Task UpdateAsync(THlabRequest objTHlabRequest, int UserId, string Username);
    }
}
