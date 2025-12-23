using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.OTManagment;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OTOperativeNotesontroller : BaseController
    {
        private readonly IOTOperativeNotes _IOTOperativeNotes;
        public OTOperativeNotesontroller(IOTOperativeNotes repository)
        {
            _IOTOperativeNotes = repository;
        }

    }
}
