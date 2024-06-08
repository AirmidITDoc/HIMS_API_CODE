using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Report;
using LinqToDB.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security;

namespace HIMS.API.Controllers.Report
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ReportController : BaseController
    {
        private readonly IGenericService<MReportConfiguration> _reportlistRepository;
        private readonly IGenericService<LoginManager> _userRepository;
        private readonly IGenericService<DoctorMaster> _doctorRepository;
        private readonly IReportService _reportService;
        public readonly IConfiguration _configuration;
        public ReportController(IGenericService<MReportConfiguration> reportlistRepository, IGenericService<LoginManager> userRepository, IGenericService<DoctorMaster> doctorRepository,
            IReportService reportService, IConfiguration configuration)
        {
            _reportlistRepository = reportlistRepository;
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
            _reportService = reportService;
            _configuration = configuration;
        }

        [HttpPost("ReportList")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> ReportList(GridRequestModel objGrid)
        {
            IPagedList<MReportConfiguration> ReportList = await _reportlistRepository.GetAllPagedAsync(objGrid);
            return Ok(ReportList.ToGridResponse(objGrid, "Report List"));
        }
        [HttpPost("UserList")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> UserList(GridRequestModel objGrid)
        {
            IPagedList<LoginManager> UserList = await _userRepository.GetAllPagedAsync(objGrid);
            return Ok(UserList.ToGridResponse(objGrid, "User List"));
        }
        [HttpPost("DoctorList")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> DoctorList(GridRequestModel objGrid)
        {
            IPagedList<DoctorMaster> DoctorList = await _doctorRepository.GetAllPagedAsync(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "Doctor List"));
        }
        [HttpPost("ViewReport")]
        public ApiResponse ViewReport(ReportRequestModel model)
        {
            model.baseUrl = Convert.ToString(_configuration["BaseUrl"]);
            model.storageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"]);
            string byteFile = _reportService.GetReportSetByProc(model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Report.", new { base64 = byteFile });
        }
    }
}
