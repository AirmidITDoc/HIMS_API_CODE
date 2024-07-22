using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DischargeTypeController : BaseController
    {
        private readonly IGenericService<DischargeTypeMaster> _repository;
        public DischargeTypeController(IGenericService<DischargeTypeMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DischargeMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DischargeTypeMaster> DischargeTypeList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DischargeTypeList.ToGridResponse(objGrid, "Discharge type List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "DischargeMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DischargeTypeId == id);
            return data.ToSingleResponse<DischargeTypeMaster, DischargeTypeModel>("DischargeType");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "DischargeMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(DischargeTypeModel obj)
        {
            DischargeTypeMaster model = obj.MapTo<DischargeTypeMaster>();
            model.IsActive = true;
            if (obj.DischargeTypeId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge Type Name added successfully.");
        }
    }
}
