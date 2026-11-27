using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OTManagement;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OTCheckListController : BaseController
    {
        private readonly IGenericService<TOtCheckList> _repository;

        public OTCheckListController(IGenericService<TOtCheckList> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]

        [Permission(PageCode = "OTReservation|OTManagement", Permission = PagePermission.View)]
       

        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TOtCheckList> OTCheckList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(OTCheckList.ToGridResponse(objGrid, "OTCheckList "));
        }
        //Add API
        [HttpPost]

        [Permission(PageCode = "OTReservation|OTManagement", Permission = PagePermission.Add)]
        

        public async Task<ApiResponse> Post(OTCheckListModel obj)
        {
            TOtCheckList model = obj.MapTo<TOtCheckList>();

            if (obj.OtcheckListId == 0)
            {

                model.CreatedBy = CurrentUserId;

                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]

        [Permission(PageCode = "OTReservation|OTManagement", Permission = PagePermission.Edit)]
        

        public async Task<ApiResponse> Edit(OTCheckListModel obj)
        {
            TOtCheckList model = obj.MapTo<TOtCheckList>();
            if (obj.OtcheckListId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;

                model.ModifiedDate = AppTime.Now;

                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
    }
}