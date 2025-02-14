using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Core.Domain.Grid;
using HIMS.API.Models.Pathology;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MPathParaRangeWithAgeMasterController : BaseController
    {
        private readonly IGenericService<MPathParaRangeWithAgeMaster> _repository;
        public MPathParaRangeWithAgeMasterController(IGenericService<MPathParaRangeWithAgeMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "TemplateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathParaRangeWithAgeMaster> MPathParaRangeWithAgeMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MPathParaRangeWithAgeMasterList.ToGridResponse(objGrid, "MPathParaRangeWithAgeMaster List"));
        }
        //List API Get By Id
        //[HttpGet("{id?}")]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository.GetById(x => x.TemplateId == id);
        //    return data.ToSingleResponse<MTemplateMaster, PathTemplateModel>("TemplateMaster");
        //}

        //Add API
        [HttpPost]
        // [Permission(PageCode = "TemplateMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MPathParaRangeWithAgeMasterModel obj)
        {
            MPathParaRangeWithAgeMaster model = obj.MapTo<MPathParaRangeWithAgeMaster>();
          //  model.IsActive = true;
            if (obj.PathparaRangeId == 0)
            {
              //  model.CreatedBy = CurrentUserId;
               // model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MPathParaRangeWithAgeMaster  added successfully.");
        }

        ////Edit API
        //[HttpPut("{id:int}")]
        ////[Permission(PageCode = "TemplateMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(MPathParaRangeWithAgeMasterModel obj)
        //{
        //    MPathParaRangeWithAgeMaster model = obj.MapTo<MPathParaRangeWithAgeMaster>();
        //  //  model.IsActive = true;
        //    if (obj.PathparaRangeId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.Updatedby = CurrentUserId;
        //     //   model.ModifiedDate = DateTime.Now;
        //        await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MPathParaRangeWithAgeMaster updated successfully.");
        //}

        ////Delete API
        //[HttpDelete]
        ////[Permission(PageCode = "RelationshipMaster", Permission = PagePermission.Delete)]
        //public async Task<ApiResponse> delete(int Id)
        //{
        //    MTemplateMaster model = await _repository.GetById(x => x.TemplateId == Id);
        //    if ((model?.TemplateId ?? 0) > 0)
        //    {
        //        model.IsActive = false;
        //        model.ModifiedBy = CurrentUserId;
        //        model.ModifiedDate = DateTime.Now;
        //        await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "TemplateMaster deleted successfully.");
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //}

    }
}
    

