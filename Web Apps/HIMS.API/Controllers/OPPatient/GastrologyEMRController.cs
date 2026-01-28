using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.API.Models.OutPatient;
using static HIMS.API.Models.IPPatient.OtbookingModelValidator;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class GastrologyEMRController : BaseController
    {
        private readonly IGastrologyEMRService _IGastrologyEMRService;
        public GastrologyEMRController(IGastrologyEMRService repository)
        {
            _IGastrologyEMRService = repository;
        }

        [HttpPost("ClinicalQuesList")]
        //[Permission(PageCode = "Payment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ClinicalQuesListDto> ClinicalQuesList = await _IGastrologyEMRService.GetListAsync(objGrid);
            return Ok(ClinicalQuesList.ToGridResponse(objGrid, "ClinicalQues List"));
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "ClinicalQuesHeader", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(GastrologyEMRModel obj)
        {
            ClinicalQuesHeader model = obj.MapTo<ClinicalQuesHeader>();
            if (obj.ClinicalQuesHeaderId == 0)
            {
                foreach (var q in model.ClinicalQuesDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }

                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IGastrologyEMRService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.Opipid);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "ClinicalQuesHeader", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(GastrologyEMRModel obj)
        {
            ClinicalQuesHeader model = obj.MapTo<ClinicalQuesHeader>();
            if (obj.ClinicalQuesHeaderId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.ClinicalQuesDetails)
                {
                    if (q.ClinicalQuesDetId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.ClinicalQuesDetId = 0;
                }


                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IGastrologyEMRService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.Opipid);
        }
        [HttpPost("Cancel")]
        //[Permission(PageCode = "ClinicalQuesHeader", Permission = PagePermission.Delete)]
        public ApiResponse Cancel(ClinicalQuesHeaderCancel obj)
        {
            ClinicalQuesHeader model = obj.MapTo<ClinicalQuesHeader>();
            if (obj.ClinicalQuesHeaderId != 0)
            {
                model.ClinicalQuesHeaderId = obj.ClinicalQuesHeaderId;
                _IGastrologyEMRService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
    }
}