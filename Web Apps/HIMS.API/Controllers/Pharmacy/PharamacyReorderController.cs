using HIMS.Api.Controllers;
using HIMS.API.Utility;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Pharmacy;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.IPPatient;
using HIMS.API.Extensions;
using Asp.Versioning;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PharamacyReorderController : BaseController
    {
        private readonly IPharmacyReorderService _IPharmacyReorderService;
       
        public PharamacyReorderController(IPharmacyReorderService repository)
        {
            _IPharmacyReorderService = repository;
           
        }
      
        [HttpPost("ItemReorderList")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<IActionResult> ListM(GridRequestModel objGrid)
        {
            IPagedList<ItemReorderListDto> ItemReorderList = await _IPharmacyReorderService.GetListAsync(objGrid);
            return Ok(ItemReorderList.ToGridResponse(objGrid, "ItemReorderList "));
        }




    }
}
