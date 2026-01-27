using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BranchController : BaseController
    {
        private readonly IBranchService _IBranchService;
        public BranchController(IBranchService repository)
        {
            _IBranchService = repository;

        }
    }
}
