﻿using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IIPAdvanceService
    {
        Task InsertAsyncSP(AdvanceHeader objAdvanceHeader,AdvanceDetail advanceDetail,Payment objpayment, int CurrentUserId, string CurrentUserName);
    }
}