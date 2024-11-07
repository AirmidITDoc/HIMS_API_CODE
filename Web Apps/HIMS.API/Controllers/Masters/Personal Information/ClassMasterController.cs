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

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ClassMasterController : BaseController
    {
        private readonly IGenericService<ClassMaster> _repository;
        public ClassMasterController(IGenericService<ClassMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
       // [Permission(PageCode = "ClassMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ClassMaster> ClassMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ClassMasterList.ToGridResponse(objGrid, "Class Master List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
       // [Permission(PageCode = "ClassMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ClassId == id);
            return data.ToSingleResponse<ClassMaster, ClassMasterModel>("Class Master");
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "ClassMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ClassMasterModel obj)
        {
            ClassMaster model = obj.MapTo<ClassMaster>();
            model.IsActive = true;
            if (obj.ClassId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Class name added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
       // [Permission(PageCode = "ClassMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ClassMasterModel obj)
        {
            ClassMaster model = obj.MapTo<ClassMaster>();
            model.IsActive = true;
            if (obj.ClassId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Class Name updated successfully.");
        }

        //Delete API
        [HttpDelete]
       // [Permission(PageCode = "ClassMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            ClassMaster model = await _repository.GetById(x => x.ClassId == Id);
            if ((model?.ClassId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Class Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }


    }
}
