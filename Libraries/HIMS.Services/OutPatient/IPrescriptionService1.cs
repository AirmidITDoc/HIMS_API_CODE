using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;


namespace HIMS.Services.OutPatient
{
    internal interface IPrescriptionService1
    {
        Task InsertAsync(TPrescription objPrescription, int CurrentUserId, string CurrentUserName);
        //Task InsertAsyncSP(TPrescription objPrescription, int CurrentUserId, string CurrentUserName);
        Task UpdateAsync(TPrescription objPrescription, int CurrentUserId, string CurrentUserName);
        //Task VerifyAsync(TPrescription objIndent, int UserId, string Username);
    
}
}
