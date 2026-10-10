using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
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
    public class VaccineInformationController : BaseController
    {
        private readonly IGenericService<TVaccineInformation> _repository;
        private readonly IVaccineInformationService _vaccineInformationService;

        public VaccineInformationController(IGenericService<TVaccineInformation> repository, IVaccineInformationService vaccineInformationService)
        {
            _repository = repository;
            _vaccineInformationService = vaccineInformationService;
        }

        // List API
        [HttpPost]
        [Route("[action]")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<VaccineInformationListDto> list = await _vaccineInformationService.GetVaccineInformationListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Vaccine Information List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _repository.GetById(x => x.VaccineId == id);
            return data.ToSingleResponse<TVaccineInformation, VaccineInformationModel>("VaccineInformation");
        }

        // Post / Insert API
        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Post(VaccineInformationModel obj)
        {
            TVaccineInformation model = obj.MapTo<TVaccineInformation>();
            model.VaccineStatus = true;
            if (obj.VaccineId == 0)
            {
                model.CreatedBy = (int)CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = (int)CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _vaccineInformationService.InsertAsync(model, (int)CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        // Edit / Update API
        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(VaccineInformationModel obj)
        {
            if (obj.VaccineId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            TVaccineInformation model = obj.MapTo<TVaccineInformation>();
            model.VaccineStatus = true;
            model.ModifiedBy = (int)CurrentUserId;
            model.ModifiedDate = AppTime.Now;

            await _vaccineInformationService.UpdateAsync(model, (int)CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.VaccineId);
        }
    }
}