using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.FeedBack
{
    public partial interface IPatientFeedBackService
    {
        Task InsertAsync(TPatientFeedback ObjTPatientFeedback, int UserId, string Username);
        Task UpdateAsync(TPatientFeedback ObjTPatientFeedback, int UserId, string Username, string[]? references);


    }
}
