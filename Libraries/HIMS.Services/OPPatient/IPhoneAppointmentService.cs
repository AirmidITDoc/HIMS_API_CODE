﻿using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public partial  interface IPhoneAppointmentService
    {
        
            Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objPhoneAppointment, int UserId, string Username);
            //Task InsertAsync(TPhoneAppointment objPhoneAppointment, int UserId, string Username);
            //Task UpdateAsync(TPhoneAppointment objPhoneAppointment, int UserId, string Username);
            Task CancelAsync(TPhoneAppointment objPhoneAppointment, int UserId, string Username);
        

    }
}