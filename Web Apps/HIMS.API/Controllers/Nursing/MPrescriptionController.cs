using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Nursing;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Nursing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MPrescriptionController : BaseController
    {
        private readonly IMPrescriptionService _IMPrescriptionService;
        public MPrescriptionController(IMPrescriptionService repository)
        {
            _IMPrescriptionService = repository;
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(MPrescriptionModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
            {
                model.RoundVisitDate = Convert.ToDateTime(obj.RoundVisitDate);
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
                //model.IsAddedBy = CurrentUserId;
                await _IMPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prescription added successfully.");
        }
        
    }
}
