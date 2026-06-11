using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ApprovalController : BaseController
    {
        private readonly IGenericService<TApprovalHeader> _repository;
        private readonly IApprovalService _IApprovalService;

        public ApprovalController(IGenericService<TApprovalHeader> repository, IApprovalService repository1)
        {
            _repository = repository;
            _IApprovalService = repository1;

        }


        [HttpPost("ApprovalList")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ApprovalListDto> ApprovalList = await _IApprovalService.GetListAsync(objGrid);
            return Ok(ApprovalList.ToGridResponse(objGrid, "Approval List"));
        }

        //Add API
        [HttpPost]
        //[Permission]
        public async Task<ApiResponse> Post(ApprovalHeaderModel obj)
        {
            TApprovalHeader model = obj.MapTo<TApprovalHeader>();
            //model.IsActive = true;
            if (obj.ApprovalId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _IApprovalService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
    }
}
