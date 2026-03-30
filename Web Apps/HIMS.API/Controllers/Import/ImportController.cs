using Asp.Versioning;
using ClosedXML.Excel;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HIMS.API.Controllers.Import
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ImportController : BaseController
    {
        public ImportController() { }
        [HttpPost("preview")]
        [Permission]
        public async Task<ApiResponse> PreviewData([FromForm] IFormFile file, [FromForm] string mapping)
        {
            if (file == null || file.Length == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "File is missing", null);

            // Deserialize mapping
            var mapList = JsonSerializer.Deserialize<List<ImportDto>>(mapping);

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid Excel file", null);

            var rows = worksheet.RangeUsed().RowsUsed().ToList();

            if (rows.Count < 2)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Excel has no data", null);

            // ✅ Step 1: Read header row
            var headerRow = rows.First();
            var headers = new Dictionary<string, int>();

            foreach (var cell in headerRow.Cells())
            {
                var header = cell.GetValue<string>()?.Trim();
                if (!string.IsNullOrEmpty(header) && !headers.ContainsKey(header))
                {
                    headers.Add(header, cell.Address.ColumnNumber);
                }
            }

            // ✅ Step 2: Validate required columns
            foreach (var map in mapList)
            {
                if (!headers.ContainsKey(map.sourceColumn))
                {
                    return ApiResponseHelper.GenerateResponse(
                        ApiStatusCode.Status400BadRequest,
                        $"Missing required column: {map.sourceColumn}",
                        null
                    );
                }
            }

            // ✅ Step 3: Read data rows
            var previewData = new List<Dictionary<string, string>>();

            int maxPreviewRows = 50; // optional limit
            int currentRow = 0;

            foreach (var row in rows.Skip(1)) // skip header
            {
                if (currentRow++ >= maxPreviewRows) break;

                var rowData = new Dictionary<string, string>();

                foreach (var map in mapList)
                {
                    var targetKey = map.targetColumn;
                    var sourceColumn = map.sourceColumn;

                    if (headers.TryGetValue(sourceColumn, out int colIndex))
                    {
                        var value = row.Cell(colIndex).GetValue<string>();
                        rowData[targetKey] = value;
                    }
                    else
                    {
                        rowData[targetKey] = null;
                    }
                }

                previewData.Add(rowData);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Files are saved successfully.", previewData);
        }
    }
}
