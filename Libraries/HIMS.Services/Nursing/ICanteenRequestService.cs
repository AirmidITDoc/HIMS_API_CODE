using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial interface ICanteenRequestService
    {
        Task InsertAsync(TCanteenRequestHeader objCanteen, int UserId, string Username);


    }
}
