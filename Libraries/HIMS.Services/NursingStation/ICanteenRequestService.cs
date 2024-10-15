using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.NursingStation
{
    public partial interface ICanteenRequestService
    {
        Task InsertAsync(TCanteenRequestHeader objCanteenRequestHeader, int UserId, string Username);
        Task InsertAsyncSP(TCanteenRequestHeader objCanteenRequestHeader, TCanteenRequestDetail objTCanteenRequestDetail ,int UserId, string Username);


    }
}
