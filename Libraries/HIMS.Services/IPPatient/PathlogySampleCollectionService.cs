﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public class PathlogySampleCollectionService : IPathlogySampleCollectionService
    {
        private readonly HIMSDbContext _context;
        public PathlogySampleCollectionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SampleCollectionPatientListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SampleCollectionPatientListDto>(model, "ps_Rtrv_PathSamPatList");

        }
        public virtual async Task<IPagedList<SampleCollectionTestListDto>> GetListAsyn(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SampleCollectionTestListDto>(model, "ps_Rtrv_PathSamColllist_Pat_Dtls");

        }

        public virtual async Task<IPagedList<LabOrRadRequestListDto>> LGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabOrRadRequestListDto>(model, "Rtrv_LabOrRadRequestList");

        }
        public virtual async Task<IPagedList<LabOrRadRequestDetailListDto>> LGetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabOrRadRequestDetailListDto>(model, "ps_Rtrv_NursingPathRadReqDet");

        }
        public virtual async Task UpdateAsyncSP(TPathologyReportHeader objTPathologyReportHeader, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = {"OpdIpdType", "SampleCollectionTime", "OpdIpdId", "PathTestId", "PathResultDr1", "PathResultDr2", "PathResultDr3", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId", "IsCompleted", "IsPrinted", "ReportDate", "ReportTime", "IsTemplateTest", "TestType", "SuggestionNotes", "AdmVisitDoctorId", "RefDoctorId", "IsVerifySign", "IsVerifyid", "IsVerifyedDate", "TPathologyReportDetails","TPathologyReportTemplateDetails"};
            var entity = objTPathologyReportHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("v_Update_PathologySampleCollection_1", CommandType.StoredProcedure , entity);
            await _context.SaveChangesAsync(UserId, Username);
        }

    }
}
