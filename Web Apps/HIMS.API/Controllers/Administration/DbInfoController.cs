using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Core.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DbInfoController : BaseController
    {

        [HttpGet]
        public IActionResult GetDbInfo()
        {
            var builder = new SqlConnectionStringBuilder(AppSettings.Settings.CONNECTION_STRING);

            return Ok(new
            {
                Server = builder.DataSource,
                Database = builder.InitialCatalog,
                Provider = "SQL Server"
            });
        }
    }
}
