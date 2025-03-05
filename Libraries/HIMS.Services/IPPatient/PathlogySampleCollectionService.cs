using HIMS.Core.Domain.Grid;
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
        public virtual async Task<IPagedList<PathologySampleCollectionDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathologySampleCollectionDto>(model, "m_Rtrv_PathSamPatList");

        }
        public virtual async Task<IPagedList<PatientTestListDto>> PGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientTestListDto>(model, "ps_Rtrv_PathPatientList_Ptnt_Dtls");

        }
        public virtual async Task UpdateAsyncSP(TPathologyReportHeader objTPathologyReportHeader, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = {"OpdIpdType", "SampleCollectionTime", "OpdIpdId", "PathTestId", "PathResultDr1", "PathResultDr2", "PathResultDr3", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedBy", "UpdatedBy", "ChargeId", "IsCompleted", "IsPrinted", "ReportDate", "ReportTime", "IsTemplateTest", "TestType", "SuggestionNotes", "AdmVisitDoctorId", "RefDoctorId", "IsVerifySign", "IsVerifyid", "IsVerifyedDate",};
            var entity = objTPathologyReportHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("v_Update_PathologySampleCollection_1", CommandType.StoredProcedure , entity);
            //objTPathologyReportHeader.PathReportId = Convert.ToInt32(PathReportId);
            await _context.SaveChangesAsync(UserId, Username);
        }

    }
}
