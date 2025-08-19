using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;

namespace HIMS.Services.Nursing
{
    public  class ClinicalCareService : IClinicalCareService
    {
        private readonly HIMSDbContext _context;
        public ClinicalCareService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<AdmisionListNursingListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdmisionListNursingListDto>(model, "m_Rtrv_AdmisionList_NursingList");
        }

        public virtual async Task<IPagedList<IPPathologyListDto>> GetListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPathologyListDto>(model, "Retrive_IP_PathologyList");
        }
        public virtual async Task<IPagedList<NursingWeightListDto>> NursingWeightList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingWeightListDto>(model, "m_Rtrv_NursingWeight");
        }
        public virtual async Task<IPagedList<NursingPainAssessmentListDto>> NursingPainAssessmentList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingPainAssessmentListDto>(model, "m_Rtrv_NursingPainAssessment");
        }

        public virtual async Task<IPagedList<NursingSugarlevelListDto>> NursingSugarlevelList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingSugarlevelListDto>(model, "m_Rtrv_NursingSugarlevel");
        }

        public virtual async Task<IPagedList<NursingVitalsListDto>> NursingVitalsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingVitalsListDto>(model, "m_Rtrv_NursingVitals");
        }


        public virtual async Task<IPagedList<NursingOxygenVentilatorListDto>> NursingOxygenVentilatorList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NursingOxygenVentilatorListDto>(model, "m_Rtrv_NursingOxygenVentilator");
        }
    }
}
