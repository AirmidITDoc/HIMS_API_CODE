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
using HIMS.API.Models.Inventory;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class PrescriptionMasterController : BaseController
    {
        private readonly IGenericService<TPrescription> _repository;
        public PrescriptionMasterController(IGenericService<TPrescription> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "TPrescription", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TPrescription> TPrescriptionList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(TPrescriptionList.ToGridResponse(objGrid, "TPrescription List"));
        }

        [HttpGet("{id?}")]
        //[Permission(PageCode = "TPrescription", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.PrecriptionId == id);
            return data.ToSingleResponse<TPrescription, PrescriptionMasterModel>("TPrescriptionList");
        }


        //Add API
        [HttpPost]
        //[Permission(PageCode = "TPrescription", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PrescriptionMasterModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();
           
            if (obj.PrecriptionId == 0)
            {
                model.IsAddBy = CurrentUserId;
                model.Date = DateTime.Now;
                model.Ptime = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Prescription added successfully.");
        }


       
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "TPrescription", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PrescriptionMasterModel obj)
        {
            TPrescription model = obj.MapTo<TPrescription>();

            if (obj.PrecriptionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.IsAddBy = CurrentUserId;
                model.Date = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "IsAddBy", "Date" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "TPrescription", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            TPrescription model = await _repository.GetById(x => x.PrecriptionId == Id);
            if ((model?.PrecriptionId ?? 0) > 0)
            {
             
                model.IsAddBy = CurrentUserId;
                model.Date = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
