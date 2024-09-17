using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface ICrossConsultationService
    {
        Task InsertAsyncSP(VisitDetail objCrossConsultation, int UserId, string Username);
    }
}
