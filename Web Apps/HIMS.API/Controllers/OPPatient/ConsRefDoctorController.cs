﻿//using Microsoft.AspNetCore.Mvc;
//using Asp.Versioning;
//using HIMS.Api.Controllers;
//using HIMS.Api.Models.Common;
//using HIMS.API.Extensions;
//using HIMS.API.Models.Masters;
//using HIMS.API.Models.OutPatient;
//using HIMS.API.Models.Pharmacy;
//using HIMS.Core;
//using HIMS.Core.Domain.Grid;
//using HIMS.Data;
//using HIMS.Data.Models;
//using HIMS.Services.OPPatient;
//using HIMS.Services.Users;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Security;
//using HIMS.API.Models.OPPatient;


//namespace HIMS.API.Controllers.OPPatient
//{
//    [Route("api/v{version:apiVersion}/[controller]")]
//    [ApiController]
//    [ApiVersion("1")]
//    public class ConsRefDoctorController : BaseController
//    {
//        private readonly IConsRefDoctorService _IConsRefDoctorServic;
//        public ConsRefDoctorController(IConsRefDoctorService repository)
//        {
//            _IConsRefDoctorServic = repository;
//        }
//        [HttpPut("ConsultantDoctorUpdate/{id:int}")]
//        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
//        public async Task<ApiResponse> UpdateAsync(ConsRefDoctorModel obj)
//        {
//            VisitDetail model = obj.MapTo<VisitDetail>();
//            if (obj.VisitId == 0)
//                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
//            else
//            {
//            await _IConsRefDoctorServic.UpdateAsync(model, CurrentUserId, CurrentUserName);
//            }
//            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ConsultantDoctor updated successfully.");
//        }


//        [HttpPut("RefDoctorUpdate")]
//        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
//        public async Task<ApiResponse> Update(RefDoctorModel obj)
//        {
//            VisitDetail model = obj.MapTo<VisitDetail>();
//            if (obj.VisitId == 0)
//                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
//            else
//            {
//            await _IConsRefDoctorServic.Update(model, CurrentUserId, CurrentUserName);
//            }
//            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RefDoctor updated successfully.");
//        }
//    }
//}
