using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPPrescriptionController : Controller
    {
        private readonly IIPPrescriptionService _IIPPrescriptionService;
       
        public IPPrescriptionController(IIPPrescriptionService repository)
        {
            _IIPPrescriptionService = repository;
            
        }
        [HttpPost("IPPrescriptionInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(NewPrescription obj)
        {
            TIpPrescription model = obj.MapTo<TIpPrescription>();
            //if (obj.TPrescription == 0)
            //{
            //    //model.BillTime = Convert.ToDateTime(obj.Pres);
            //    //model.AddedBy = CurrentUserId;
            //   // await _IIPPrescriptionService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            //}
            //else
            //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bill added successfully.");
        }

    }
}
