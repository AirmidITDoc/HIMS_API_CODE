﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public partial interface ICrossConsultationService
    {
        Task<VisitDetail> InsertAsyncSP(VisitDetail objCrossConsultation, int UserId, string Username);
    }
}