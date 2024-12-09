using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Administration;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdministrationController: BaseController
    {
        
            private readonly IAdministrationService _IAdministrationService;
            public AdministrationController(IAdministrationService repository)
            {
                _IAdministrationService = repository;
            }
          

    }
}
