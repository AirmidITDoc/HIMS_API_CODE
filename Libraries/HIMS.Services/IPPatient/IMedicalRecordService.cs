﻿using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial interface IMedicalRecordService
    {
        Task InsertAsync(TIpmedicalRecord objTIpmedicalRecord, int UserId, string Username);
        Task UpdateAsync(TIpmedicalRecord objTIpmedicalRecord, int UserId, string Username);
    }
}
