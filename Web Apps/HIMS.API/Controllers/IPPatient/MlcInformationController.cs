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
using HIMS.API.Models.IPPatient;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MlcInformationController : BaseController
    {
        private readonly IGenericService<TMlcinformation> _repository;
        public MlcInformationController(IGenericService<TMlcinformation> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MlcInfo", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TMlcinformation> MlcinformationList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MlcinformationList.ToGridResponse(objGrid, "Mlcinformation List "));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MlcInfo", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.AdmissionId == id);
            if (data == null){
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status404NotFound, "No data found.");
            }
            return data.ToSingleResponse<TMlcinformation, MlcInformationModel>("TMlcinformation");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "MlcInfo", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MlcInformationModel obj)
        {
            TMlcinformation model = obj.MapTo<TMlcinformation>();
            //model.IsActive = true;
            if (obj.Mlcid == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.AdmissionId);
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "MlcInfo", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MlcInformationModel obj)
        {
            TMlcinformation model = obj.MapTo<TMlcinformation>();
            //model.IsActive = true;
            if (obj.Mlcid == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.", model.AdmissionId);
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "MlcInfo", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TMlcinformation model = await _repository.GetById(x => x.Mlcid == Id);
            if ((model?.Mlcid ?? 0) > 0)
            {
                //model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
