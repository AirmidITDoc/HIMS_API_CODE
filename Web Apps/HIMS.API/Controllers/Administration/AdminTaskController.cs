using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using Asp.Versioning;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdminTaskController : BaseController
    {
        private readonly IAdminTaskService _IAdminTaskService;
      
        public AdminTaskController(IAdminTaskService repository)
        {
            _IAdminTaskService = repository;
         
        }
    }
}
