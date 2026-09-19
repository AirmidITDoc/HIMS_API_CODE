using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.DietKitchen;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data;
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
    }

}



