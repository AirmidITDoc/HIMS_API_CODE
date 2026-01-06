using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.GastrologyService
{
    public partial interface IQuestionMasterService
    {
        Task InsertAsync(MSubQuestionMaster ObjMSubQuestionMaster, int UserId, string Username);
        Task UpdateAsync(MSubQuestionMaster ObjMSubQuestionMaster, int UserId, string Username, string[]? references);
    }
}
