using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
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
    }
}
