using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DietKitchen;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.GRN;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using HIMS.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.DietMaster
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class DietMenuMasterController : BaseController
    {
        private readonly IDietMenuMasterService _DietMenuMasterService;
        private readonly IGenericService<MDietMenuMaster> _repository;

        public DietMenuMasterController(IDietMenuMasterService repository, IGenericService<MDietMenuMaster> repository1)
        {
            _DietMenuMasterService = repository;
            _repository = repository1;


        }



        [HttpPost("DietmenumasterList")]
        [Permission]
        public async Task<IActionResult> ListAsync(GridRequestModel objGrid)
        {
            IPagedList<DietmenumasterListDto> List1 = await _DietMenuMasterService.GetDietmenumasterList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Diet Menu master  List"));
        }
         
        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Insert(DietmenumasterModel obj)
        {
            MDietMenuMaster model = obj.MapTo<MDietMenuMaster>();

            if (obj.DietMenuId == 0)
            {
                if (model.MDietMenuDetailMasters != null)
                {
                    foreach (var q in model.MDietMenuDetailMasters)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                }

                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;

                await _DietMenuMasterService.InsertAsync(
                    model,
                    CurrentUserId,
                    CurrentUserName);

                return ApiResponseHelper.GenerateResponse(
                    ApiStatusCode.Status200OK,
                    "Record added successfully.");
            }

            return ApiResponseHelper.GenerateResponse(
                ApiStatusCode.Status500InternalServerError,
                "Invalid params");
        }


        [HttpPut("Edit/{id:int}")]
        public async Task<ApiResponse> Edit(DietmenumasterModel obj)
        {
            if (obj.DietMenuId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            MDietMenuMaster model = obj.MapTo<MDietMenuMaster>();


            foreach (var q in model.MDietMenuDetailMasters)
            {
                q.MenuDetId = 0;
             //   q.OrderDate = AppTime.Now;
            }

            model.ModifiedDate = AppTime.Now;
            model.ModifiedBy = CurrentUserId;

            await _DietMenuMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.DietMenuId);
        }
    }

}



