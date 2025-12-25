using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
//using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ClinicalCareController : BaseController
    {
        private readonly IClinicalCareService _ClinicalCareService;

        private readonly IGenericService<TNursingVital> _repository;
        private readonly IGenericService<TNursingSugarLevel> _repository2;
        private readonly IGenericService<TNursingOrygenVentilator> _repository3;
        private readonly IGenericService<TNursingPainAssessment> _repository4;
        private readonly IGenericService<TNursingWeight> _repository5;



        public ClinicalCareController(IClinicalCareService repository, IGenericService<TNursingVital> repository1, IGenericService<TNursingSugarLevel> repository2, IGenericService<TNursingOrygenVentilator> repository3,
            IGenericService<TNursingPainAssessment> repository4, IGenericService<TNursingWeight> repository5)
        {
            _repository = repository1;
            _repository2 = repository2;
            _repository3 = repository3;
            _repository4 = repository4;
            _repository5 = repository5;





            _ClinicalCareService = repository;

        }

        [HttpPost("AdmisionListNursingList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PrescriptionReturnList(GridRequestModel objGrid)
        {
            IPagedList<AdmisionListNursingListDto> AdmisionListNursingList = await _ClinicalCareService.GetListAsync(objGrid);
            return Ok(AdmisionListNursingList.ToGridResponse(objGrid, "AdmisionListNursingList "));
        }

        [HttpPost("IPPathologyList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IPPathologyList(GridRequestModel objGrid)
        {
            IPagedList<IPPathologyListDto> IPPathologyList = await _ClinicalCareService.GetListAsync1(objGrid);
            return Ok(IPPathologyList.ToGridResponse(objGrid, "IPPathologyList "));
        }


        [HttpPost("NursingWeightList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingWeightList(GridRequestModel objGrid)
        {
            IPagedList<NursingWeightListDto> NursingWeightList = await _ClinicalCareService.NursingWeightList(objGrid);
            return Ok(NursingWeightList.ToGridResponse(objGrid, "NursingWeightList "));
        }

        [HttpPost("NursingPainAssessmentList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingPainAssessmentList(GridRequestModel objGrid)
        {
            IPagedList<NursingPainAssessmentListDto> NursingPainAssessmentList = await _ClinicalCareService.NursingPainAssessmentList(objGrid);
            return Ok(NursingPainAssessmentList.ToGridResponse(objGrid, "NursingPainAssessmentList "));
        }

        [HttpPost("NursingSugarlevelList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingSugarlevelList(GridRequestModel objGrid)
        {
            IPagedList<NursingSugarlevelListDto> NursingSugarlevelList = await _ClinicalCareService.NursingSugarlevelList(objGrid);
            return Ok(NursingSugarlevelList.ToGridResponse(objGrid, "NursingSugarlevelList "));
        }

        [HttpPost("NursingVitalsList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingVitalsList(GridRequestModel objGrid)
        {
            IPagedList<NursingVitalsListDto> NursingVitalsList = await _ClinicalCareService.NursingVitalsList(objGrid);
            return Ok(NursingVitalsList.ToGridResponse(objGrid, "NursingVitalsList"));
        }

        [HttpPost("NursingOxygenVentilatorList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> NursingOxygenVentilatorList(GridRequestModel objGrid)
        {
            IPagedList<NursingOxygenVentilatorListDto> NursingOxygenVentilatorList = await _ClinicalCareService.NursingOxygenVentilatorList(objGrid);
            return Ok(NursingOxygenVentilatorList.ToGridResponse(objGrid, "NursingOxygenVentilatorList"));
        }


        [HttpPost("NursingVitalInsert")]
        //    [Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Posts(NursingVitalsModel obj)
        {
            TNursingVital model = obj.MapTo<TNursingVital>();
            if (obj.VitalId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _ClinicalCareService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record  added successfully.");
        }

        [HttpPut("NursingVitalUpdate/{id:int}")]
        //[Permission(PageCode = "NursingNote", Permission = PagePermission.Edit)
        public async Task<ApiResponse> Edit(NursingVitalsModel obj)
        {
            TNursingVital model = obj.MapTo<TNursingVital>();
            if (obj.VitalId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPost("TNursingVitalCancel")]

        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public ApiResponse Delete(NursingVitalsDeleteModel obj)
        {
            TNursingVital model = obj.MapTo<TNursingVital>();
            if (obj.VitalId != 0)
            {

                _ClinicalCareService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Deleted successfully.");
        }

        [HttpPost("TNursingSugarLevelInsert")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(NursingSugarLevelModel obj)
        {
            TNursingSugarLevel model = obj.MapTo<TNursingSugarLevel>();
            if (obj.Id == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository2.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("NursingSugarLevelUpdate/{id:int}")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(NursingSugarLevelModel obj)
        {
            TNursingSugarLevel model = obj.MapTo<TNursingSugarLevel>();
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository2.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }



        [HttpPost("TNursingSugarLevelCancel")]

        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public ApiResponse Deletes(NursingSugarDeleteModel obj)
        {
            TNursingSugarLevel model = obj.MapTo<TNursingSugarLevel>();
            if (obj.Id != 0)
            {

                _ClinicalCareService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Deleted successfully.");
        }

        [HttpPost("NursingOrygenVentilatorInsert")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Posts(NursingOrygenVentilatorModel obj)
        {
            TNursingOrygenVentilator model = obj.MapTo<TNursingOrygenVentilator>();
            if (obj.Id == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository3.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("NursingOrygenVentilatorUpdate/{id:int}")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edits(NursingOrygenVentilatorModel obj)
        {
            TNursingOrygenVentilator model = obj.MapTo<TNursingOrygenVentilator>();
            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository3.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpPost("TNursingOrygenVentilatorCancel")]

        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public ApiResponse Cancel(TNursingOrygenVentilatorDeleteModel obj)
        {
            TNursingOrygenVentilator model = obj.MapTo<TNursingOrygenVentilator>();
            if (obj.Id != 0)
            {

                _ClinicalCareService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Deleted successfully.");
        }


        [HttpPost("NursingPainAssessmentInsert")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(NursingPainAssessmentModel obj)
        {
            TNursingPainAssessment model = obj.MapTo<TNursingPainAssessment>();
            if (obj.PainAssessmentId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository4.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("NursingPainAssessmentUpdate/{id:int}")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(NursingPainAssessmentModel obj)
        {
            TNursingPainAssessment model = obj.MapTo<TNursingPainAssessment>();
            if (obj.PainAssessmentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository4.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPost("TNursingPainAssessmentCancel")]

        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public ApiResponse Cancel(TNursingPainAssessmentDeleteModel obj)
        {
            TNursingPainAssessment model = obj.MapTo<TNursingPainAssessment>();
            if (obj.PainAssessmentId != 0)
            {

                _ClinicalCareService.Cancel1(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Deleted successfully.");
        }


        [HttpPost("NursingWeightInsert")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Posts(TNursingWeightModel obj)
        {
            TNursingWeight model = obj.MapTo<TNursingWeight>();
            if (obj.PatWeightId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository5.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("NursingWeightUpdate/{id:int}")]
        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edits(TNursingWeightModel obj)
        {
            TNursingWeight model = obj.MapTo<TNursingWeight>();
            if (obj.PatWeightId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository5.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }



        [HttpPost("TNursingWeightCancel")]

        //   [Permission(PageCode = "BankMaster", Permission = PagePermission.Add)]
        public ApiResponse Cancel(TNursingWeightDeleteModel obj)
        {
            TNursingWeight model = obj.MapTo<TNursingWeight>();
            if (obj.PatWeightId != 0)
            {

                _ClinicalCareService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Deleted successfully.");
        }

    }
}

