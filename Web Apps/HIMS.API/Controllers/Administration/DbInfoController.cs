using Asp.Versioning;
using HIMS.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DbInfoController : BaseController
    {
        private readonly IConfiguration _configuration;
        public DbInfoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetDbInfo()
        {

            var connStr = _configuration.GetConnectionString("CONNECTION_STRING");
            var builder = new SqlConnectionStringBuilder(connStr);

            return Ok(new
            {
                Server = builder.DataSource,
                Database = builder.InitialCatalog,
                Provider = "SQL Server"
            });
        }
    }
}
