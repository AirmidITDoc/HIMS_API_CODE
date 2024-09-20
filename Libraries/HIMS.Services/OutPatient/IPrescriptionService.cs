using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;


namespace HIMS.Services.OutPatient
{
    internal interface IPrescriptionService
    {
       
        Task InsertAsync(TPrescription objPrescription, int UserId, string Username);
        //Task InsertAsyncSP(TPrescription objPrescription, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(TPrescription objPrescription, int UserId, string Username);
        //Task List(TPrescription objPrescription, int UserId, string Username);
        //Task Get(TPrescription objPrescription, int UserId, string Username);
        //Task Delete(TPrescription objPrescription, int UserId, string Username);
        //Task VerifyAsync(TPrescription objIndent, int UserId, string Username);

    }
}
