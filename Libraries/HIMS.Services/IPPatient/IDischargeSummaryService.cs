using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IDischargeSummaryService
    {
        Task InsertAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username);
        Task UpdateAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username);
        Task InsertAsyncTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username);
        Task UpdateAsyncTemplate(DischargeSummary ObjDischargeTemplate, TIpPrescriptionDischarge ObjTIpPrescriptionTemplate, int UserId, string Username);

        Task<IPagedList<DischrageSummaryListDTo>> IPDischargesummaryList(GridRequestModel objGrid);
        Task<IPagedList<IPPrescriptiononDischargeListDto>> IPPrescriptionDischargesummaryList(GridRequestModel objGrid);

    }
}




