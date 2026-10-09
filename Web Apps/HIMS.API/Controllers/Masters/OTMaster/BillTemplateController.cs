using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.OTMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BillTemplateController : BaseController
    {
        private readonly IGenericService<MOtBillTemplate> _repository;
        private readonly IBillTemplateService _IBillTemplateService;

        public BillTemplateController(IGenericService<MOtBillTemplate> repository, IBillTemplateService repository1)
        {
            _repository = repository;
            _IBillTemplateService = repository1;
        }
        [HttpPost("OtBillTemplateList")]
        //[Permission]
        public async Task<IActionResult> OtBillTemplateList(GridRequestModel objGrid)
        {
            IPagedList<OtBillTemplateListDto> OtBillTemplateList = await _IBillTemplateService.GetListAsync(objGrid);
            return Ok(OtBillTemplateList.ToGridResponse(objGrid, "OtBillTemplate List"));
        }

        

        // Add API
        [HttpPost]
        // [Permission]
        public async Task<ApiResponse> Post(List<BillTemplateModel> obj)
        {
            foreach (var item in obj)
            {
                MOtBillTemplate model = item.MapTo<MOtBillTemplate>();

                if (item.TemplateId == 0)
                {
                    model.CreatedBy = CurrentUserId;
                    model.CreatedDateTime = AppTime.Now;
                    model.ModifiedBy = CurrentUserId;
                    model.ModifiedDateTime = AppTime.Now;

                    await _repository.Add(model, CurrentUserId, CurrentUserName);
                }
                else
                {
                    return ApiResponseHelper.GenerateResponse(  ApiStatusCode.Status500InternalServerError,  "Invalid params");
                }
            }

            return ApiResponseHelper.GenerateResponse(
                ApiStatusCode.Status200OK,
                "Records added successfully.");
        }
        // Edit API
        [HttpPut("{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(List<BillTemplateModel> obj)
        {
            foreach (var item in obj)
            {
                MOtBillTemplate model = item.MapTo<MOtBillTemplate>();

                if (item.TemplateId == 0)return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status500InternalServerError, "Invalid params");

                model.ModifiedBy = CurrentUserId;
                model.ModifiedDateTime = AppTime.Now;

                await _repository.Update( model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDateTime" });
            }

            return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK, "Records updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission]
        public async Task<ApiResponse> Delete(int Id)
        {
            MOtBillTemplate model = await _repository.GetById(x => x.TemplateId == Id);
            if ((model?.TemplateId ?? 0) > 0)
            {
                //model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDateTime = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
