using ClosedXML.Excel;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Common;
using System.Text.Json;

namespace HIMS.API.Utility
{
    public static class ExcelImportHelper
    {
        public static async Task<ApiResponse> GetDataFromExcelAsync<T>(IFormFile file, string mapping, Func<T, (int status, string message)> validateFunc, int maxRows = 50) where T : new()
        {
            if (file == null || file.Length == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "File is missing", null);


            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid Excel file", null);
            // Deserialize mapping
            var mapList = JsonSerializer.Deserialize<List<ImportDto>>(mapping);

            var rows = worksheet.RangeUsed().RowsUsed().ToList();

            var headerRow = rows.First();
            var headers = new Dictionary<string, int>();

            foreach (var cell in headerRow.Cells())
            {
                var header = cell.GetValue<string>()?.Trim();
                if (!string.IsNullOrEmpty(header) && !headers.ContainsKey(header))
                    headers.Add(header, cell.Address.ColumnNumber);
            }

            var result = new List<T>();
            int currentRow = 0;

            foreach (var row in rows.Skip(1))
            {
                if (currentRow++ >= maxRows) break;

                T obj = new();

                foreach (var map in mapList)
                {
                    if (!headers.TryGetValue(map.sourceColumn, out int colIndex))
                        continue;

                    var value = row.Cell(colIndex).GetValue<string>();

                    var prop = typeof(T).GetProperties()
                        .FirstOrDefault(p => p.Name.Equals(map.targetColumn, StringComparison.OrdinalIgnoreCase));

                    if (prop != null && value != null)
                    {
                        var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        try
                        {
                            object safeValue = string.IsNullOrWhiteSpace(value)
                                ? null
                                : Convert.ChangeType(value, targetType);

                            prop.SetValue(obj, safeValue);
                        }
                        catch
                        {
                            // optional: handle conversion error
                        }
                    }
                }

                // ✅ Apply validation
                var (status, message) = validateFunc(obj);

                // set Status & Message dynamically
                typeof(T).GetProperty("Status")?.SetValue(obj, status);
                typeof(T).GetProperty("Message")?.SetValue(obj, message);

                result.Add(obj);
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Files are saved successfully.", result);
        }
    }
}
