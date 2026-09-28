using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DietKitchen;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.API.Models.MRD;
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

        [HttpPost("DietmenumasterDetailsList")]
        [Permission]
        public async Task<IActionResult> ListAsync1(GridRequestModel objGrid)
        {
            IPagedList<DietmenuDetailmasterListDto> List1 = await _DietMenuMasterService.GetDietmenumasterDetailsList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Diet Menu master details  List"));
        }

       
        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Insert(DietmenumasterModels obj)

        {
            MDietMenuMaster model = obj.Dietmenumaster.MapTo<MDietMenuMaster>();
            List<MDietMenuDetailMaster> ObjDietMenuDetailMasters = obj.DietMenuDetailMasters.MapTo<List<MDietMenuDetailMaster>>();


            if (model.DietMenuId == 0)
            {
                model.CreatedBy = CurrentUserId;
                await _DietMenuMasterService.InsertAsync(model, ObjDietMenuDetailMasters, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.DietMenuId);
        }



       
        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(int id, DietmenumasterUpdateModels obj)
        {
            MDietMenuMaster model = obj.Dietmenumaster.MapTo<MDietMenuMaster>();
            List<MDietMenuDetailMaster> ObjDietMenuDetailMasters = obj.DietMenuDetailMasters.MapTo<List<MDietMenuDetailMaster>>();

            model.DietMenuId = id;

            if (model.DietMenuId != 0)
            {
                model.ModifiedBy = CurrentUserId;

                await _DietMenuMasterService.UpdateAsync(model, ObjDietMenuDetailMasters, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.DietMenuId);
        }
    }

}



