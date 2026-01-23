using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pathology;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.IPPatient;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class EstimateController : BaseController
    {
        private readonly IEstimasteService _IEstimasteService;

        private readonly IGenericService<TEstimateHeader> _repository;

        public EstimateController(IEstimasteService repository, IGenericService<TEstimateHeader> repository1)
        {
            _IEstimasteService = repository;
            _repository = repository1;

        }

        [HttpPost("EstimateList")]
        //[Permission(PageCode = "EstimateList", Permission = PagePermission.View)]
        public async Task<IActionResult> EstimateList(GridRequestModel objGrid)
        {
            IPagedList<EstimateListDto> EstimateList = await _IEstimasteService.EstimateListAsync(objGrid);
            return Ok(EstimateList.ToGridResponse(objGrid, "Estimate List "));
        }

        [HttpPost("EstimateDetailsList")]
        //[Permission(PageCode = "EstimateDetailsList", Permission = PagePermission.View)]
        public async Task<IActionResult> EstimateDetailsList(GridRequestModel objGrid)
        {
            IPagedList<EstimateDetailsListDto> EstimateDetailsList = await _IEstimasteService.EstimateDetailsListAsync(objGrid);
            return Ok(EstimateDetailsList.ToGridResponse(objGrid, "Estimate Details List "));
        }

       
        //Add API
        [HttpPost("Insert")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(EstimateModel obj)
        {
            TEstimateHeader model = obj.MapTo<TEstimateHeader>();
            if (obj.EstimateId == 0)
            {
                foreach (var q in model.TEstimateDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IEstimasteService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.EstimateId);
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(EstimateModel obj)
        {
            TEstimateHeader model = obj.MapTo<TEstimateHeader>();
            if (obj.EstimateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TEstimateDetails)
                {
                    if (q.EstimateDetId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.EstimateDetId = 0;
                }

                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IEstimasteService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.EstimateId);
        }


    }
}
