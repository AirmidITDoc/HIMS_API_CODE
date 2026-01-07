using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class GastrologyEMRController : BaseController
    {
        private readonly IGastrologyEMRService _IGastrologyEMRService;
        public GastrologyEMRController(IGastrologyEMRService repository)
        {
            _IGastrologyEMRService = repository;
        }

        [HttpPost("ClinicalQuesList")]
        //[Permission(PageCode = "Payment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ClinicalQuesListDto> ClinicalQuesList = await _IGastrologyEMRService.GetListAsync(objGrid);
            return Ok(ClinicalQuesList.ToGridResponse(objGrid, "ClinicalQues List"));
        }
    }
}
