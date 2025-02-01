using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial  interface IOPDPrescriptionService
    {
        Task InsertAsyncSP(TPrescription objTPrescription, int UserId, string Username);
        //Task<TPrescription> GetById(int Id);


    }
}
