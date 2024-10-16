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
    public class PriscriptionController : BaseController
    {

        private readonly IPriscriptionService _IPriscriptionService;
        public PriscriptionController(IPriscriptionService repository)
        {
            _IPriscriptionService = repository;
        }

        //[HttpPost("InsertSP")]
        ////[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertSP(PriscriptionModel obj)
        //{
        //    TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
        //    if (obj.PresReId == 0)
        //    {
        //        model.PresTime = Convert.ToDateTime(obj.PresTime);
        //        model.Addedby = CurrentUserId;
        //        await _IPriscriptionService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Priscription added successfully.");
        //}
        [HttpPost("Insert")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PriscriptionModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.Addedby = CurrentUserId;
                await _IPriscriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Priscription added successfully.");
        }
    }
}

