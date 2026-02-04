using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Pathology;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabApprovalController : BaseController
    {
        private readonly ILabApprovalService _ILabApprovalService;

        public LabApprovalController(ILabApprovalService repository)
        {
            _ILabApprovalService = repository;
        }

        [HttpPost("LabResultCompletedList")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LabResultCompletedListDto> LabResultCompletedList = await _ILabApprovalService.GetListAsync(objGrid);
            return Ok(LabResultCompletedList.ToGridResponse(objGrid, "LabResultCompleted List"));
        }
       
    }
}
