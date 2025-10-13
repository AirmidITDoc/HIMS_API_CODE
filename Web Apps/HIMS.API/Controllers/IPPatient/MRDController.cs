using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.MRD;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.IPPatient
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class MRDController : BaseController
    {

        private readonly I_MRDCertificate _I_MRDCertificate;

        public MRDController(I_MRDCertificate repository)
        {
            _I_MRDCertificate = repository;
           
        }

        [HttpPost("MRDList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetMRDDetailList(GridRequestModel objGrid)
        {
            var MRDList = await _I_MRDCertificate.MRDList(objGrid);
            // 🧠 Handle null or empty
            //if (MRDList == null || !MRDList.Any())
            //{
            //    return StatusCode(StatusCodes.Status404NotFound, new
            //    {
            //        statusCode = 404,
            //        message = "No data found",
            //        data = new
            //        {
            //            data = new List<MRDList>(),
            //            recordsFiltered = 0,
            //            recordsTotal = 0,
            //            pageIndex = 0
            //        }
            //    });
            //}

            // ✅ Success
            return Ok(MRDList.ToGridResponse(objGrid, "MRD List"));

        }

    }
}
