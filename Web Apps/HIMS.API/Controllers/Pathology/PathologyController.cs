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
using HIMS.Services.Pathlogy;
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
        private readonly IPathlogyService _IPathlogyService;


        public PathologyController(IPathlogySampleCollectionService repository, ILabRequestService repository1, IPathlogyService repository2)
        {
            _IPathlogySampleCollectionService = repository;
            _ILabRequestService = repository1;
            _IPathlogyService = repository2;
        }
        [HttpPost("PathTemplateForUpdateList")]
        //[Permission(PageCode = "PathTemplateForUpdateList", Permission = PagePermission.View)]
        public async Task<IActionResult> PathTemplateForUpdateList(GridRequestModel objGrid)
        {
            IPagedList<PathTemplateForUpdateListDto> PathTestForUpdateList = await _IPathlogyService.PathTemplateForUpdateList(objGrid);
            return Ok(PathTestForUpdateList.ToGridResponse(objGrid, "PathTemplateForUpdate List"));
        }


        [HttpPost("PathTestForUpdateList")]
        //[Permission(PageCode = "PathTemplateForUpdateList", Permission = PagePermission.View)]
        public async Task<IActionResult> PathTestForUpdateList(GridRequestModel objGrid)
        {
            IPagedList<PathTestForUpdateListdto> PathTestForUpdateList= await _IPathlogyService.PathTestForUpdateList(objGrid);
            return Ok(PathTestForUpdateList.ToGridResponse(objGrid, "PathTestForUpdate List"));
        }

        [HttpPost("PathParaFillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathParaFillListList(GridRequestModel objGrid)
        {
            IPagedList<PathParaFillListDto> PathParaFillList = await _IPathlogyService.PathParaFillList(objGrid);
            return Ok(PathParaFillList.ToGridResponse(objGrid, "PathParaFillList App List"));
        }
        [HttpPost("PathSubtestFillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathSubtestFillList(GridRequestModel objGrid)
        {
            IPagedList<PathSubtestFillListDto> PathSubtestFillList = await _IPathlogyService.PathSubtestFillList(objGrid);
            return Ok(PathSubtestFillList.ToGridResponse(objGrid, "PathSubtestFill App List"));
        }
        [HttpPost("PathologyTestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathologyTestList(GridRequestModel objGrid)
        {
            IPagedList<PathologyTestListDto> PathologyTestList = await _IPathlogyService.PathologyTestList(objGrid);
            return Ok(PathologyTestList.ToGridResponse(objGrid, "PathologyTest App List"));
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

        [HttpPost("PathologyPatientTestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PatientList(GridRequestModel objGrid)
        {
            IPagedList<PatientTestListDto> PatientList = await _IPathlogySampleCollectionService.PGetListAsync(objGrid);
            return Ok(PatientList.ToGridResponse(objGrid, "PathologyPatientTestList "));
        }

    }
}
