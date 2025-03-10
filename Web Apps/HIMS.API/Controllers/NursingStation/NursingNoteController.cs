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
using HIMS.API.Models.Nursing;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NursingNoteController : BaseController
    {
        private readonly IGenericService<TNursingNote> _repository;
        public NursingNoteController(IGenericService<TNursingNote> repository)
        {
            _repository = repository;
        }
        ////List API
        //[HttpPost]
        //[Route("[action]")]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        //public async Task<IActionResult> List(GridRequestModel objGrid)
        //{
        //    IPagedList<TNursingNote> PatientTypeList = await _repository.GetAllPagedAsync(objGrid);
        //    return Ok(PatientTypeList.ToGridResponse(objGrid, "TNursingNoteList"));
        //}
        ////List API Get By Id
        //[HttpGet("{id?}")]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository.GetById(x => x.DocNoteId == id);
        //    return data.ToSingleResponse<TNursingNote, NursingNoteModel>("TNursingNote");
        //}
        ////Add API
        //[HttpPost]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Post(NursingNoteModel obj)
        //{
        //    TNursingNote model = obj.MapTo<TNursingNote>();
        //    //model.IsActive = true;
        //    if (obj.DocNoteId == 0)
        //    {
        //        model.CreatedBy = CurrentUserId;
        //        model.ModifiedDateTime = DateTime.Now;
        //        await _repository.Add(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  added successfully.");
        //}
        ////Edit API
        //[HttpPut("{id:int}")]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(NursingNoteModel obj)
        //{
        //    TNursingNote model = obj.MapTo<TNursingNote>();
        //    //model.IsActive = true;
        //    if (obj.DocNoteId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.ModifiedBy = CurrentUserId;
        //        model.CreatedDatetime = DateTime.Now;
        //        await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDatetime" });
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  updated successfully.");
        //}
        ////Delete API
        //[HttpDelete]
        ////[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        //public async Task<ApiResponse> Delete(int Id)
        //{
        //    TNursingNote model = await _repository.GetById(x => x.DocNoteId == Id);
        //    if ((model?.DocNoteId ?? 0) > 0)
        //    {
        //        //model.IsActive = false;
        //        model.ModifiedBy = CurrentUserId;
        //        model.CreatedDatetime = DateTime.Now;
        //        await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NursingNote  deleted successfully.");
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //}

    }
}
