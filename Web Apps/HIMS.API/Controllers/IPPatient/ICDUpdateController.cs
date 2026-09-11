using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Services.MRD;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.IPPatient
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ICDUpdateController : BaseController
    {

        private readonly I_ICDUpdateService _ICDUpdateService;

        public ICDUpdateController(I_ICDUpdateService repository)
        {
            _ICDUpdateService = repository;
        }


        [HttpPost("InsertICD")]
        //   [Permission]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertICD(ICDupdateModel obj)
        {

            TPatIcdcdeH Model = obj.TPatIcdcdeH.MapTo<TPatIcdcdeH>();
            List<TPatIcdcdeD> DetModel = obj.TPatIcdcdeD.MapTo<List<TPatIcdcdeD>>();

            if (obj.TPatIcdcdeH.Hid == 0)
            {

                Model.ReqDate = Convert.ToDateTime(obj.TPatIcdcdeH.ReqDate);
                Model.ReqTime = Convert.ToDateTime(obj.TPatIcdcdeH.ReqTime);
                Model.AddedBy = CurrentUserId;
                Model.UpdatedBy = CurrentUserId;
                await _ICDUpdateService.InsertICDSp(Model, DetModel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", Model);
        } 
    }
}
