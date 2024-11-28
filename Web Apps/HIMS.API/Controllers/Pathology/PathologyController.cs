using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Services.Common;
using HIMS.Services.IPPatient;
using HIMS.Services.Nursing;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyController : BaseController
    {
        private readonly IPathlogySampleCollectionService _IPathlogySampleCollectionService;
        private readonly ILabRequestService _ILabRequestService;

        public PathologyController(IPathlogySampleCollectionService repository, ILabRequestService repository1)
        {
            _IPathlogySampleCollectionService = repository;
            _ILabRequestService = repository1;


        }

        [HttpPost("SampleCollectionList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SampleCollectionList(GridRequestModel objGrid)
        {
            IPagedList<PathologySampleCollectionDto> SampleCollectionList = await _IPathlogySampleCollectionService.GetListAsync(objGrid);
            return Ok(SampleCollectionList.ToGridResponse(objGrid, "PrescriptionReturnList "));
        }
        [HttpPost("LabOrRadRequestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabOrRadRequestList(GridRequestModel objGrid)
        {
            IPagedList<LabOrRadRequestListDto> LabOrRadRequestList = await _ILabRequestService.LGetListAsync(objGrid);
            return Ok(LabOrRadRequestList.ToGridResponse(objGrid, "LabOrRadRequestList "));
        }

        [HttpPost("PatientList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PatientList(GridRequestModel objGrid)
        {
            IPagedList<PatientListDto> PatientList = await _IPathlogySampleCollectionService.PGetListAsync(objGrid);
            return Ok(PatientList.ToGridResponse(objGrid, "PatientList "));
        }

    }
}
