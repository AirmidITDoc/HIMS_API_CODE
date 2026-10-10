using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Nursing.IPEMR;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ImmunizatoinInfoController : BaseController
    {
        private readonly IImmunizatoinInfoService _immunizationService;

        public ImmunizatoinInfoController(IImmunizatoinInfoService immunizationService)
        {
            _immunizationService = immunizationService;
        }

        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _immunizationService.GetByIdAsync(id);
            return data.ToSingleResponse<TImmunizatoinInfo, ImmunizatoinInfoModel>("ImmunizatoinInfo");
        }

        




        [HttpPost("SaveImmunizationInfo")]
        [Permission]
        public async Task<ApiResponse> SaveImmunizationInfo(List<ImmunizatoinInfoModel> obj)
        {
            if (obj == null || obj.Count == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            long? regId = obj.FirstOrDefault()?.RegId;

            if (regId == null || regId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "RegId is required");
            }

            List<TImmunizatoinInfo> models = new();

            foreach (var item in obj)
            {
                TImmunizatoinInfo model = item.MapTo<TImmunizatoinInfo>();

                model.ImmunizatoinId = 0;
                model.RegId = regId;
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                models.Add(model);
            }

            await _immunizationService.ImmunizationInfoAsync(models, regId.Value, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Immunization info saved successfully.");
        }
    }
}