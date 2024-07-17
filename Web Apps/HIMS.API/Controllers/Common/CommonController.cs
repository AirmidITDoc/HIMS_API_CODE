using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CommonController : BaseController
    {
        private readonly ICommonService _ICommonService;
        public CommonController(ICommonService commonRepository)
        {
            _ICommonService = commonRepository;
        }

        [HttpPost]
        public ApiResponse GetByProc(ListRequestModel model)
        {
            dynamic resultList = _ICommonService.GetDataSetByProc(model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.mode + " List.", (dynamic)resultList);
        }
        [Route("get-daily-dashboard-data")]
        [HttpPost]
        public ApiResponse GetListByProc(ListRequestModel model)
        {
            string pDashboardId = model?.SearchFields?.FirstOrDefault(x => x.FieldName == "pDashboardId")?.FieldValue ?? "";
            if (pDashboardId == "1")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.mode + " List.", _ICommonService.GetSingleListByProc<DashboardDto1>(model));
            else if (pDashboardId == "2")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.mode + " List.", _ICommonService.GetSingleListByProc<DashboardHospitalDto>(model));
            else if (pDashboardId == "3")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.mode + " List.", _ICommonService.GetSingleListByProc<DashboardDoctorDto>(model));
            else if (pDashboardId == "3")
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, model.mode + " List.", _ICommonService.GetSingleListByProc<DashboardOperativeDto>(model));
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "invalid data");
        }
    }
    public class DashboardDto1
    {
        public DateTime? BillDate { get; set; }
        public int BillMonth { get; set; }
        public int BillYear { get; set; }
        public decimal Ipd { get; set; }
        public decimal Opd { get; set; }
        public decimal Pharma { get; set; }
        public decimal Total { get { return Ipd + Opd + Pharma; } }
    }
    public class DashboardHospitalDto
    {
        public DateTime? BillDate { get; set; }
        public int BillMonth { get; set; }
        public int BillYear { get; set; }
        public int HospitalPatient { get; set; }
        public int PrivatePatient { get; set; }
        public int ReferalPatient { get; set; }
        public int Total { get { return HospitalPatient + PrivatePatient + ReferalPatient; } }
        public decimal HospitalPatientPer { get { return Convert.ToDecimal(((decimal)HospitalPatient * 100 / Total).ToString("F")); } }
        public decimal PrivatePatientPer { get { return Convert.ToDecimal(((decimal)PrivatePatient * 100 / Total).ToString("F")); } }
        public decimal ReferalPatientPer { get { return Convert.ToDecimal(((decimal)ReferalPatient * 100 / Total).ToString("F")); } }
    }
    public class DashboardDoctorDto
    {
        public string DoctorName { get; set; }
        public int TotalPatients { get; set; }
        public int Jan { get; set; }
        public int Feb { get; set; }
        public int Mar { get; set; }
        public int Apr { get; set; }
        public int May { get; set; }
        public int Jun { get; set; }
        public int Jul { get; set; }
        public int Aug { get; set; }
        public int Sep { get; set; }
        public int Oct { get; set; }
        public int Nov { get; set; }
        public int Dec { get; set; }
        public decimal PerJan { get; set; }
        public decimal PerFeb { get; set; }
        public decimal PerMar { get; set; }
        public decimal PerApr { get; set; }
        public decimal PerMay { get; set; }
        public decimal PerJun { get; set; }
        public decimal PerJul { get; set; }
        public decimal PerAug { get; set; }
        public decimal PerSep { get; set; }
        public decimal PerOct { get; set; }
        public decimal PerNov { get; set; }
        public decimal PerDec { get; set; }
    }
    public class DashboardOperativeDto
    {
        public string BillMonthName { get; set; }
        public int BillMonth { get; set; }
        public int BillYear { get; set; }
        public int Medicine { get; set; }
        public int Operative { get; set; }
        public int Total { get { return Medicine + Operative; } }
        public decimal PerOperative { get { return Convert.ToDecimal(((decimal)Operative * 100 / Total).ToString("F")); } }
        public decimal PerMedicine { get { return Convert.ToDecimal(((decimal)Medicine * 100 / Total).ToString("F")); } }
    }
}
