using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Billing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TarrifMasterController : BaseController
    {
        private readonly IGenericService<TariffMaster> _repository;
        public TarrifMasterController(IGenericService<TariffMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "TariffMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TariffMaster> TariffMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TariffMasterList.ToGridResponse(objGrid, "Tarif List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "TariffMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TariffId == id);
            return data.ToSingleResponse<TariffMaster, TarifMasterModel>("TarifMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "TariffMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(TarifMasterModel obj)
        {
            TariffMaster model = obj.MapTo<TariffMaster>();
            model.IsActive = true;
            if (obj.TariffId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "TariffMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TarifMasterModel obj)
        {
            TariffMaster model = obj.MapTo<TariffMaster>();
            model.IsActive = true;
            if (obj.TariffId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        //Delete API
        [HttpDelete]
        [Permission(PageCode = "TariffMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TariffMaster model = await _repository.GetById(x => x.TariffId == Id);
            if ((model?.TariffId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }







    }
}
