using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.API.Models.Nursing;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorNoteController : BaseController
    {
        private readonly IGenericService<TDoctorsNote> _repository;
        public DoctorNoteController(IGenericService<TDoctorsNote> repository)
        {
            _repository = repository;
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "DoctorsNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(DoctorNoteModel obj)
        {
            TDoctorsNote model = obj.MapTo<TDoctorsNote>();
            //model.IsActive = true;
            if (obj.DoctNoteId == 0)
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
        [Permission(PageCode = "DoctorsNote", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DoctorNoteModel obj)
        {
            TDoctorsNote model = obj.MapTo<TDoctorsNote>();
            //model.IsActive = true;
            if (obj.DoctNoteId == 0)
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
        [Permission(PageCode = "DoctorsNote", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            TDoctorsNote model = await _repository.GetById(x => x.DoctNoteId == Id);
            if ((model?.DoctNoteId ?? 0) > 0)
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
