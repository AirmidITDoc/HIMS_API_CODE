using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IDischargeSummaryService
    {
        Task InsertAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username);
        Task UpdateAsyncSP(DischargeSummary ObjDischargeSummary, List<TIpPrescriptionDischarge> ObjTIpPrescriptionDischarge, int UserId, string Username);
        Task InsertAsyncTemplate(DischargeSummary ObjDischargeTemplate, List<TIpPrescriptionDischarge> ObjTIpPrescriptionTemplate, int UserId, string Username);
        Task UpdateAsyncTemplate(DischargeSummary ObjDischargeTemplate, TIpPrescriptionDischarge ObjTIpPrescriptionTemplate, int UserId, string Username);
        Task InsertDischargeSP(Discharge ObjDischarge, Admission ObjAdmission, Bedmaster ObjBedmaster, int currentUserId, string currentUserName);
        Task UpdateDischargeSP(Discharge ObjDischarge, Admission ObjAdmission ,int currentUserId, string currentUserName);
        Task UpdateAsync(InitiateDischarge ObjInitiateDischarge, int UserId, string Username);
        Task DischargeInsertAsyncSP(InitiateDischarge ObjInitiateDischarge,  int UserId, string Username);
        Task<IPagedList<PatientClearanceAprovViewListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PatientClearanceApprovalListDto>> GetListAsyncP(GridRequestModel objGrid);
        Task<IPagedList<DischrageSummaryListDTo>> IPDischargesummaryList(GridRequestModel objGrid);
        Task<IPagedList<IPPrescriptiononDischargeListDto>> IPPrescriptionDischargesummaryList(GridRequestModel objGrid);

    }
}




