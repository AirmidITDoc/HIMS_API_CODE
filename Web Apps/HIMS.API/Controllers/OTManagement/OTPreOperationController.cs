using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.OTManagement;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTPreOperationController : BaseController
    {

        private readonly IOTPreOperationService _IOTPreOperationService;


        public OTPreOperationController(IOTPreOperationService repository)
        {
            _IOTPreOperationService = repository;



        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OTPreOperationModel obj)
        {
            TOtPreOperationHeader model = obj.MapTo<TOtPreOperationHeader>();
            if (obj.OtpreOperationId == 0)
            {
                foreach (var q in model.TOtPreOperationAttendingDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtPreOperationCathlabDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtPreOperationDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtPreOperationSurgeryDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }

                model.CreatedDate = DateTime.Now;
                model.Createdby = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTPreOperationService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
    }
}
