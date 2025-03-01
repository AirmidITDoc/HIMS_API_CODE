using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IDischargeSummaryService
    {
        Task InsertAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username);
        Task UpdateAsyncSP(DischargeSummary ObjDischargeSummary, TIpPrescriptionDischarge ObjTIpPrescriptionDischarge, int UserId, string Username);
        Task InsertAsyncTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username);
        Task UpdateAsyncTemplate(DischargeSummary ObjDischargeTemplate, TIpPrescriptionDischarge ObjTIpPrescriptionTemplate, int UserId, string Username);


    }
}
