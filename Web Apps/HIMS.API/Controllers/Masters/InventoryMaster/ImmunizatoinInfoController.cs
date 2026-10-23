using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Nursing.IPEMR;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
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

        [HttpPost("ImmunizationList")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ImmunizatoinInfoDto> list = await _immunizationService.GetVaccineInformationListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Immunization  Information List"));
        }


        [HttpPost("SaveImmunizationInfo")]
        //[Permission]
        public async Task<ApiResponse> SaveImmunizationInfo(List<ImmunizatoinInfoModel> obj)
        {
            if (obj == null || obj.Count == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            long? Opipid = obj.FirstOrDefault()?.Opipid;

            if (Opipid == null || Opipid == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Opipid is required");
            }

            List<TImmunizatoinInfo> models = new();

            foreach (var item in obj)
            {
                TImmunizatoinInfo model = item.MapTo<TImmunizatoinInfo>();

                model.ImmunizatoinId = 0;
                model.Opipid = Opipid;
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                models.Add(model);
            }

            await _immunizationService.ImmunizationInfoAsync(models, Opipid.Value, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Immunization info saved successfully.");
        }
    }
}