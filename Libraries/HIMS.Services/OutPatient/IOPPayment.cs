using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
   
         public partial interface IOPPayment
    {
   
        void InsertSP(Payment objPayment, int UserId, string Username);
        //Task InsertAsync(Payment objPayment, int UserId, string Username);
       // Task UpdateAsync(Payment objPayment, int UserId, string Username);
    }
}
