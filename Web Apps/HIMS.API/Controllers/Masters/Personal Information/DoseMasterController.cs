using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using Asp.Versioning;

namespace HIMS.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoseMasterController : BaseController
    {
        private readonly IGenericService<MDoseMaster> _repository;
        public DoseMasterController(IGenericService<MDoseMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
       // [Permission(PageCode = "DoseMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MDoseMaster> DoseMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DoseMasterList.ToGridResponse(objGrid, "Dose List"));
        }
        [HttpGet("{id?}")]
       // [Permission(PageCode = "DoseMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DoseId == id);
            return data.ToSingleResponse<MDoseMaster, DoseMasterModel>("DoseMaster");
        }
         [HttpPost]
       // [Permission(PageCode = "DoseMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(DoseMasterModel obj)
        {
            MDoseMaster model = obj.MapTo<MDoseMaster>();
            model.IsActive = true;
            if (obj.DoseId == 0)
            {
                //model.CreatedBy = CurrentUserId;
                //model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Dose added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
       // [Permission(PageCode = "DoseMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoseMasterModel obj)
        {
            MDoseMaster model = obj.MapTo<MDoseMaster>();
            model.IsActive = true;
            if (obj.DoseId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Dose updated successfully.");
        }
        //Delete API
        [HttpDelete]
      //  [Permission(PageCode = "DoseMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MDoseMaster model = await _repository.GetById(x => x.DoseId == Id);
            if ((model?.DoseId ?? 0) > 0)
            {
                model.IsActive = false;
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Dose deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
