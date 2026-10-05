using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing.IPEMR;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.MRD;
using HIMS.Data.DTO.Nursing.IPEMR;
using HIMS.Data.Models;
using HIMS.Services.Nursing.IPEMR;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation.IPEMR
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class EMRController : BaseController
    {
        private readonly IEMRService _EmrService;
        private readonly IGenericService<TIpEmrfamilyMedicalHistory> _repository;

        public EMRController(IEMRService emrService, IGenericService<TIpEmrfamilyMedicalHistory> repository)
        {
            _EmrService = emrService;
            _repository = repository;
        }

        [HttpPost("DiagnosisInfoList")]
        [Permission]
        public async Task<IActionResult> DiagnosisinfoList(GridRequestModel objGrid)
        {
            IPagedList<IPEMRDiagnosisInfoListDto> IPEMRDiagnosisInfoListDto = await _EmrService.GetIPEMRDiagnosisInfoAsync(objGrid);
            return Ok(IPEMRDiagnosisInfoListDto.ToGridResponse(objGrid, "IPEMRDiagnosisInfo List"));
        }

        [HttpPost("DignosisHistoryList")]
        [Permission]
        public async Task<IActionResult> DiagnosisHistoryList(GridRequestModel objGrid)
        {
            IPagedList<IPEMRDignosisHistoryListDto> IPEMRDignosisHistoryListDto = await _EmrService.GetIPEMRDignosisHistoryAsync(objGrid);
            return Ok(IPEMRDignosisHistoryListDto.ToGridResponse(objGrid, "IPEMRDignosisHistory List"));
        }

        [HttpPost("FamilyMedicalHistoryList")]
        [Permission]
        public async Task<IActionResult> FamilyMedicalHistoryList(GridRequestModel objGrid)
        {
            IPagedList<IPEMRFamilyMedicalHistoryListDto> IPEMRFamilyMedicalHistoryListDto = await _EmrService.GetIPEMRFamilyMedicalHistoryAsync(objGrid);
            return Ok(IPEMRFamilyMedicalHistoryListDto.ToGridResponse(objGrid, "IPEMRFamilyMedicalHistory List"));
        }



        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _EmrService.GetByIdAsync(id);
            return data.ToSingleResponse<TIpEmrhistory, EMRModel>("TIpEmrhistory");
        }

       

        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(EMRModel obj)
        {
            TIpEmrhistory model = obj.MapTo<TIpEmrhistory>();
            if (obj.IpdEmrId == 0)
            {
                foreach (var q in model.TIpEmrdiagnosisInfos)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TIpEmrdignosisHistories)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                //foreach (var q in model.TIpEmrfamilyMedicalHistories)
                //{
                //    q.CreatedBy = CurrentUserId;
                //    q.CreatedDate = AppTime.Now;

                //}

                foreach (var q in model.TIpEmrVitals)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }

                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _EmrService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.IpdEmrId);
        }

        [HttpPut("Edit/{id:int}")]
       // [Permission]
        public async Task<ApiResponse> Edit(EMRModel obj)
        {
            TIpEmrhistory model = obj.MapTo<TIpEmrhistory>();
            if (obj.IpdEmrId == 0)
            {

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            else
            {
                foreach (var q in model.TIpEmrdiagnosisInfos)
                {
                    if (q.IpemrdiagnId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.IpemrdiagnId = 0;
                }

                foreach (var v in model.TIpEmrdignosisHistories)
                {
                    if (v.EmrdignId == 0)
                    {
                        v.CreatedBy = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.EmrdignId = 0;
                }
                //foreach (var v in model.TIpEmrfamilyMedicalHistories)
                //{
                //    if (v.FhistId == 0)
                //    {
                //        v.CreatedBy = CurrentUserId;
                //        v.CreatedDate = AppTime.Now;
                //    }
                //    v.ModifiedBy = CurrentUserId;
                //    v.ModifiedDate = AppTime.Now;
                //    v.FhistId = 0;
                //}
                foreach (var v in model.TIpEmrVitals)
                {
                    if (v.IpemrVitalId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.IpemrVitalId = 0;
                }
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _EmrService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.IpdEmrId);
        }


        [HttpPost("InsertFamilyHistory")]
        // [Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertFamilyHistory(List<EMRFamilyMedicalHistoryModel> obj)
        {
            foreach (var item in obj)
            {
                TIpEmrfamilyMedicalHistory model = item.MapTo<TIpEmrfamilyMedicalHistory>();

                if (item.FhistId == 0)
                {
                    model.CreatedDate = AppTime.Now;
                    model.CreatedBy = CurrentUserId;

                    await _EmrService.InsertFamilyHistoryAsync(model, CurrentUserId, CurrentUserName);
                }
                else
                {
                    return ApiResponseHelper.GenerateResponse(
                        ApiStatusCode.Status500InternalServerError,
                        "Invalid params");
                }
            }

            return ApiResponseHelper.GenerateResponse(
                ApiStatusCode.Status200OK,
                "Records added successfully.");
        }

        // Edit / Update API
        [HttpPut("Edit/FamilyHistory")]
        //[Permission]
        public async Task<ApiResponse> Edit(List<EMRFamilyMedicalHistoryModel> obj)
        {
            foreach (var item in obj)
            {
                if (item.FhistId == 0)
                {
                    return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status500InternalServerError,"Invalid params");
                }

                TIpEmrfamilyMedicalHistory model = item.MapTo<TIpEmrfamilyMedicalHistory>();

                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                await _EmrService.UpdateFamilyHistoryAsync(model, CurrentUserId, CurrentUserName,  new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK,"Records updated successfully.");
        }


        [HttpPost("FamilyHistory")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TIpEmrfamilyMedicalHistory> TIpEmrfamilyMedicalHistoryList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TIpEmrfamilyMedicalHistoryList.ToGridResponse(objGrid, "Family History List "));
        }

        [HttpPost("FamilyHistoryList")]
       //[Permission]
        public async Task<IActionResult> FamilyHistoryList(GridRequestModel objGrid)
        {
            IPagedList<FamilyMedicalHistoryListDto> FamilyMedicalHistoryList = await _EmrService.FamilyMedicalHistoryListAsync(objGrid);
            return Ok(FamilyMedicalHistoryList.ToGridResponse(objGrid, "Family Medical History List "));
        }

    }
}