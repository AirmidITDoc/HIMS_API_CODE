﻿using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Nursing
{
    public partial interface IPriscriptionReturnService
    {
        Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);
        Task UpdateAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);

    }
}