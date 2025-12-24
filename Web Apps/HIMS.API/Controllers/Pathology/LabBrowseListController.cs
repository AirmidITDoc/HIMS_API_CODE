using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabBrowseListController : BaseController
    {
        private readonly ILabBrowseListService _ILabBrowseList;

        public LabBrowseListController(ILabBrowseListService ILabBrowseList)
        {
            _ILabBrowseList = ILabBrowseList;
        }

        //List API
        [HttpPost("LabBillList")]
        //[Permission(PageCode = "TOtOperativeNote", Permission = PagePermission.View)]
        public async Task<IActionResult> GetListAsync(GridRequestModel objGrid)
        {
            IPagedList<Services.OTManagment.BrowseLABBillListDto> Servicelist = await _ILabBrowseList.GetLabListListAsync(objGrid);
            return Ok(Servicelist.ToGridResponse(objGrid, " Lab List "));
        }

    }
}
