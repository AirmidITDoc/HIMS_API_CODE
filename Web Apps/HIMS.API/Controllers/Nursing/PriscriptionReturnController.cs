using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.Nursing;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Nursing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PriscriptionReturnController : BaseController
    {

        private readonly IPriscriptionReturnService _IPriscriptionReturnService;
        public PriscriptionReturnController(IPriscriptionReturnService repository)
        {
            _IPriscriptionReturnService = repository;
        }

       
        [HttpPost("Insert")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.Addedby = CurrentUserId;
                await _IPriscriptionReturnService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Priscription added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.PresDate = Convert.ToDateTime(obj.PresDate);
                await _IPriscriptionReturnService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Priscription updated successfully.");
        }
    }
}

