using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;

namespace HIMS.Services.Nursing
{
    public partial interface IClinicalCareService
    {
        Task<IPagedList<AdmisionListNursingListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<IPPathologyListDto>> GetListAsync1(GridRequestModel objGrid);
        Task<IPagedList<NursingWeightListDto>> NursingWeightList(GridRequestModel objGrid);
        Task<IPagedList<NursingPainAssessmentListDto>> NursingPainAssessmentList(GridRequestModel objGrid);
        Task<IPagedList<NursingSugarlevelListDto>> NursingSugarlevelList(GridRequestModel objGrid);
        Task<IPagedList<NursingVitalsListDto>> NursingVitalsList(GridRequestModel objGrid);
        Task<IPagedList<NursingOxygenVentilatorListDto>> NursingOxygenVentilatorList(GridRequestModel objGrid);
        Task InsertAsync(TNursingVital ObjTNursingVital, int UserId, string Username);
        void Cancel(TNursingVital objTNursingVital, int UserId, string Username);

        void Cancel(TNursingSugarLevel objTNursingSugarLevel, int UserId, string Username);


        void Cancel1(TNursingPainAssessment objTNursingPainAssessment, int UserId, string Username);

        void Cancel(TNursingWeight objTNursingWeight, int UserId, string Username);
        void Cancel(TNursingOrygenVentilator objTNursingOrygenVentilator, int UserId, string Username);





        








    }
}
