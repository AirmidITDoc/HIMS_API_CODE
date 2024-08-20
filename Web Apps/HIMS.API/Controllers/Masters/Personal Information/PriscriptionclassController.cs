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

    public class PriscriptionclassController : BaseController
    {
        private readonly IGenericService<MClassMaster> _repository;
        public PriscriptionclassController(IGenericService<MClassMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MClassMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MClassMaster> MClassMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MClassMasterList.ToGridResponse(objGrid, "MClassMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MClassMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ClassId == id);
            return data.ToSingleResponse<MClassMaster, PriscriptionclassModel>("MClassMasterList");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "MClassMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PriscriptionclassModel obj)
        {
            MClassMaster model = obj.MapTo<MClassMaster>();
            model.IsActive = true;
            if (obj.ClassId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " ClassName added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "MClassMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PriscriptionclassModel obj)
        {
            MClassMaster model = obj.MapTo<MClassMaster>();
            model.IsActive = true;
            if (obj.ClassId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " ClassName updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "MClassMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MClassMaster model = await _repository.GetById(x => x.ClassId == Id);
            if ((model?.ClassId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ClassName deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
