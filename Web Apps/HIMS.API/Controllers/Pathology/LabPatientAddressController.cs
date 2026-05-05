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
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabPatientAddressController : BaseController
    {
        private readonly IGenericService<TLabPatientAddress> _repository;
        private readonly ILabPatientAddresService _ILabPatientAddresService;

        public LabPatientAddressController(ILabPatientAddresService repository1, IGenericService<TLabPatientAddress> repository)
        {
            _ILabPatientAddresService = repository1;

            _repository = repository;
        }
        [HttpPost("LabPatientAddressList")]
        //[Permission]
        public async Task<IActionResult> salesdetaillist(GridRequestModel objGrid)
        {
            IPagedList<LabPatientAddressDto> LabPatientAddressList = await _ILabPatientAddresService.GetListAsync(objGrid);
            return Ok(LabPatientAddressList.ToGridResponse(objGrid, "LabPatientAddress List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.AddressId == id);
            return data.ToSingleResponse<TLabPatientAddress, LabPatientAddressModel>("TLabPatientAddress");
        }
        //Add API
        [HttpPost]
        //[Permission]
        public async Task<ApiResponse> Post(LabPatientAddressModel obj)
        {
            TLabPatientAddress model = obj.MapTo<TLabPatientAddress>();
            model.IsActive = true;
            if (obj.AddressId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(LabPatientAddressModel obj)
        {
            TLabPatientAddress model = obj.MapTo<TLabPatientAddress>();
            model.IsActive = true;
            if (obj.AddressId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission]
        public async Task<ApiResponse> Delete(int Id)
        {
            TLabPatientAddress model = await _repository.GetById(x => x.AddressId == Id);
            if ((model?.AddressId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
