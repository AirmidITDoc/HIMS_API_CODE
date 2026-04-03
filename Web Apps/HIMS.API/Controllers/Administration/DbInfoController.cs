using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
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
        [HttpGet("DBInformation")]
        public async Task<ApiResponse> DBInformation()
        {
            var builder = new SqlConnectionStringBuilder(AppSettings.Settings.CONNECTION_STRING);

            var dbInfo = new
            {
                Server = builder.DataSource,
                Database = builder.InitialCatalog,
                UserName = builder.UserID,
                Provider = "SQL Server"
            };

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"Database info fetched successfully.",dbInfo);
        }
    }
}
