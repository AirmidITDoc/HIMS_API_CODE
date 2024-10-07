using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public partial interface IPrescription
    {
        Task InsertAsyncSP(TPrescription objPrescription, int UserId, string Username);
        Task UpdateAsync(TPrescription objPrescription, int UserId, string Username);
        Task InsertAsync(TPrescription objPrescription, int UserId, string Username);
        Task InsertAsyncSP(TPrescription objPrescription, VisitDetail objVisitDetail, int CurrentUserId, string CurrentUsername);

    }
}
