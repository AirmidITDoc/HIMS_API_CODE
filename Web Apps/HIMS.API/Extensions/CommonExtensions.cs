using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using HIMS.API.Models.Common;
using HIMS.Core;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Dynamic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Core.Infrastructure;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Aspose.Cells;
using ClosedXML.Excel;
using System.Formats.Asn1;
using System.Globalization;
using System.Web;
using CsvHelper;
using DocumentFormat.OpenXml.Office2010.Ink;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HIMS.API.Extensions
{
    public static class CommonExtensions
    {
        public static HttpContext HttpContextAccessor => new HttpContextAccessor().HttpContext;
        private static readonly DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //public static object ToGridResponse<T>(this IPagedList<T> GridData, GridRequestModel objGrid, string FileName)
        //{
        //    try
        //    {
        //        //return new ApiResponse { StatusCode = 200, Data = GridData.ToGrid() };
        //        return objGrid.ExportType == 0 ? new ApiResponse { StatusCode = 200, Data = GridData.ToGrid() } : GridData.ExportToExcel(objGrid, FileName);
        //    }
        //    catch (Exception)
        //    {
        //        return new object();
        //    }
        //}
        public static object ToGridResponse<T>(this IPagedList<T> list, GridRequestModel objGrid, string Msg = "Grid Data")
        {
            if (objGrid.ExportType == ExportType.Excel)
            {
                return ExportToExcel(list, objGrid.Columns, "Sheet 1");
            }
            else if (objGrid.ExportType == ExportType.Pdf)
            {
                return ExportToPdf(list, objGrid.Columns);
            }
            else if (objGrid.ExportType == ExportType.Csv)
            {
                return ExportToCsv(list, objGrid.Columns);
            }
            else
            {
                return new
                {
                    StatusCode = (int)ApiStatusCode.Status200OK,
                    StatusText = Msg,
                    Data = list.ToGrid()
                };
            }
        }


        //public static object ToGridResponse<T, TResult>(this IPagedList<T> GridData, GridRequestModel objGrid, string FileName, Func<T, TResult> selector)
        //{
        //    try
        //    {
        //        //return new ApiResponse { StatusCode = 200, Data = GridData.ToGrid(selector) };
        //        return objGrid.ExportType == 0 ? new ApiResponse { StatusCode = 200, Data = GridData.ToGrid(selector) } : GridData.ExportToGrid(objGrid, FileName);
        //    }
        //    catch (Exception)
        //    {
        //        return new object();
        //    }
        //}
        public static GridResponseModel ToGrid<T>(this IPagedList<T> GridData)
        {
            try
            {
                return new GridResponseModel() { Data = GridData, RecordsFiltered = GridData.TotalCount, RecordsTotal = GridData.TotalCount, PageIndex = GridData.PageIndex };
            }
            catch (Exception)
            {
                return new GridResponseModel();
            }
        }
        public static ObjectResult ToResponse<T>(this T obj, string msg, object additionalData = null)
        {
            var resObj = GetResponse<T>(obj, msg, additionalData);
            return new ObjectResult(resObj)
            {
                StatusCode = resObj.StatusCode
            };
        }
        private static ApiResponse GetResponse<T>(this T obj, string msg, object additionalData = null)
        {
            if (!object.Equals(obj, default(T)))
            {
                return new ApiResponse() { StatusCode = (int)ApiStatusCode.Status200OK, StatusText = ApiStatusCode.Status200OK.ToString(), Message = msg, Data = obj, ExtraData = additionalData };
            }
            else
            {
                return new ApiResponse() { StatusCode = (int)ApiStatusCode.Status404NotFound, StatusText = ApiStatusCode.Status404NotFound.ToString(), Message = "Not Found", Data = obj, ExtraData = null };
            }
        }
        public static ApiResponse ToSingleResponse<T>(this T obj, string msg)
        {
            try
            {
                return ApiResponseHelper.GenerateResponse(obj is null ? ApiStatusCode.Status404NotFound : ApiStatusCode.Status200OK, obj is null ? "Not Found" : string.Format("Data loaded", msg), obj);
            }
            catch (Exception)
            {
                return new ApiResponse();
            }
        }
        public static ApiResponse ToSingleResponse<T, TDto>(this T obj, string msg)
        {
            try
            {
                return ApiResponseHelper.GenerateResponse(obj is null ? ApiStatusCode.Status404NotFound : ApiStatusCode.Status200OK, obj is null ? "Not Found" : string.Format("Data loaded", msg), obj.MapTo<TDto>());
            }
            catch (Exception)
            {
                return new ApiResponse();
            }
        }
        public static IList<SelectListItem> ToDropDown<T>(this IList<T> drpList, string dropValColName = "Id", string dropDisColName = "Name", string drpGrpColName = "")
        {
            var valueProperties = dropValColName.Split('|');

            return drpList.Select(x => new SelectListItem
            {
                Value = GetConcatenatedValue(x, valueProperties),
                Text = GetPropertyValue(x, dropDisColName),
                Group = new SelectListGroup
                {
                    Name = !string.IsNullOrEmpty(drpGrpColName) ? GetPropertyValue(x, drpGrpColName) : string.Empty
                }
            }).ToList();
        }

        private static string GetPropertyValue<T>(T item, string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            return property?.GetValue(item)?.ToString() ?? string.Empty;
        }
        private static string GetConcatenatedValue<T>(T item, string[] propertyNames)
        {
            return string.Join("|", propertyNames.Select(propertyName => GetPropertyValue(item, propertyName)));
        }
        public static List<int> ToIntList(this object a, string separator)
        {
            try
            {
                return string.IsNullOrWhiteSpace(Convert.ToString(a)) ? new List<int>() : a.ToString().Split(separator.ToCharArray()).Select(int.Parse).ToList();
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }
        public static List<SelectListItem> ToSelectListItems(this Type enumType)
        {
            List<SelectListItem> items = new();
            foreach (Enum cur in Enum.GetValues(enumType))
            {
                items.Add(new SelectListItem()
                {
                    Text = cur.ToString().Replace('_', ' '),
                    Value = GetEnumValue(cur)
                });
            }
            return items;
        }
        public static string GetEnumValue(this Enum EnumType)
        {
            return Convert.ToString((int)(object)EnumType);
        }
        public static int ToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal ToDecimal(this object obj)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ToDouble(this object a)
        {
            try
            {
                return Convert.ToDouble(a);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public static int GetId(HttpRequest request)
        {
            int Id = 0;

            HttpRequestRewindExtensions.EnableBuffering(request);
            try
            {
                if ((request?.ContentType?.Contains("form-data") ?? false))
                {
                    Id = request.Form["Id"].ToString().ToInt();
                }
                else
                {
                    using StreamReader reader = new(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
                    string strRequestBody = reader.ReadToEnd();
                    Id = JObject.Parse(strRequestBody)["Id"].ToInt();
                }
            }
            catch (Exception) { }
            finally
            {
                request.Body.Position = 0;
            }
            return Id;
        }



        public static DateTime FromUnixTime(long unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }
        public static void ReplaceTemplateStringByToken(ref string EmailTemplateWithToken, string ReplaceTokenKey, string ReplaceTokenValue)
        {
            EmailTemplateWithToken = EmailTemplateWithToken.Replace("{{" + ReplaceTokenKey + "}}", string.IsNullOrWhiteSpace(ReplaceTokenValue) ? "" : ReplaceTokenValue);
        }

        public static void CopyProperties<EntityBase>(this EntityBase source, EntityBase destination, string[] Skipped = null)
        {
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                if (Skipped == null || !Skipped.Contains(destinationPi.Name))
                {
                    PropertyInfo sourcePi = source.GetType().GetProperty(destinationPi.Name);
                    destinationPi.SetValue(destination, sourcePi.GetValue(source, null), null);
                }
            }
        }


        public static List<dynamic> ToDynamic(this DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                //--------- change from here
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
                //--------- change up to here
            }
            return dynamicDt;
        }

        public static string GenerateToken(LoginManager user, string Secret, int Minutes, string permissions)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new("Id",EncryptionUtility.EncryptText(user.UserId.ToString(),SecurityKeys.EnDeKey)),
                   new("UserToken",EncryptionUtility.EncryptText(user.UserToken,SecurityKeys.EnDeKey)),
                   new("Permissions",EncryptionUtility.EncryptText(permissions,SecurityKeys.EnDeKey)),
                   new("RoleId", EncryptionUtility.EncryptText(user.WebRoleId.Value.ToString(),SecurityKeys.EnDeKey)),
                   new("UserName",EncryptionUtility.EncryptText(user.UserName,SecurityKeys.EnDeKey)),
                   new("FullName", EncryptionUtility.EncryptText(user.FirstName + (!string.IsNullOrEmpty(user.LastName) ? " " + user.LastName : ""),SecurityKeys.EnDeKey))
                }),
                Expires = DateTime.UtcNow.AddMinutes(Minutes <= 0 ? 30 : Minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
        public static bool CheckPermission(string pageCode, PagePermission permit)
        {
            ClaimsPrincipal currentUser = HttpContextAccessor.User;
            if (currentUser.HasClaim(c => c.Type == "RoleId") && currentUser.HasClaim(c => c.Type == "Permissions"))
            {
                string permissions = EncryptionUtility.DecryptText(currentUser.Claims?.FirstOrDefault(c => c.Type == "Permissions")?.Value ?? "", SecurityKeys.EnDeKey);
                string[] AllPermissions = permissions.Split(',');
                var Perms = AllPermissions.Where(x => pageCode.Split('|').Contains(x.Split('|')[0])).Select(x => new { IsAdd = x.Split('|')[1], IsEdit = x.Split('|')[2], IsDelete = x.Split('|')[3], IsView = x.Split('|')[4] });
                if (permit == PagePermission.Add)
                {
                    return Perms.Any(x => x.IsAdd == "1");
                }
                else if (permit == PagePermission.Edit)
                {
                    return Perms.Any(x => x.IsEdit == "1");
                }
                else if (permit == PagePermission.Delete)
                {
                    return Perms.Any(x => x.IsDelete == "1");
                }
                else if (permit == PagePermission.View)
                {
                    return Perms.Any(x => x.IsView == "1");
                }
            }
            return false;
        }

        #region :: Export Functions ::

        private static Stream ExportToExcel<T>(IPagedList<T> list, List<Core.Domain.Grid.GridColumn> columns, string sheetName = "Sheet 1")
        {
            using var excel = new XLWorkbook();
            var workSheet = excel.Worksheets.Add(sheetName);

            // Create and style the header row
            AddHeader(workSheet, columns);
            StyleHeaderRow(workSheet);

            // Start from row 2 because row 1 is the header
            int rowIndex = 2;
            foreach (var item in list)
            {
                PopulateRow(workSheet, item, columns, rowIndex);
                workSheet.Row(rowIndex).Height = 30;
                rowIndex++;
            }

            return SaveToStream(excel);
        }
        private static void AddHeader(IXLWorksheet workSheet, List<Core.Domain.Grid.GridColumn> columns)
        {
            int colIndex = 1;
            foreach (var column in columns.Where(c => !c.Name.Equals("Action", StringComparison.OrdinalIgnoreCase)))
            {
                workSheet.Cell(1, colIndex++).Value = column.Name;
            }
        }
        private static void StyleHeaderRow(IXLWorksheet workSheet)
        {
            var headerRow = workSheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Height = 30;
        }

        private static void PopulateRow<T>(IXLWorksheet workSheet, T item, List<Core.Domain.Grid.GridColumn> columns, int rowIndex)
        {
            var propInfoList = item.GetType().GetProperties();
            int colIndex = 1;

            foreach (var column in columns)
            {
                if (!column.Name.Equals("Action", StringComparison.OrdinalIgnoreCase) && propInfoList.Any(x => string.Equals(x.Name, column.Data, StringComparison.OrdinalIgnoreCase)))
                {
                    var propInfo = propInfoList.FirstOrDefault(x => string.Equals(x.Name, column.Data, StringComparison.OrdinalIgnoreCase));
                    if (propInfo != null)
                    {
                        AddCellValue(workSheet, propInfo, item, rowIndex, colIndex++);
                    }
                }
            }
        }

        private static void AddCellValue(IXLWorksheet workSheet, PropertyInfo propInfo, object item, int rowIndex, int colIndex)
        {
            var auditProps = propInfo.GetCustomAttribute<AuditLogAttribute>();
            var value = propInfo.GetValue(item);
            if (value != null)
            {
                if (propInfo.PropertyType == typeof(DateTime) || propInfo.PropertyType == typeof(DateTime?))
                {
                    DateTime cellValue = Convert.ToDateTime(propInfo.GetValue(item));// GetDateTimeValue(propInfo, item, auditProps, objGrid);
                    workSheet.Cell(rowIndex, colIndex).Style.NumberFormat.Format = auditProps?.ExportFormat ?? "dd MMM yyyy h:mm tt";
                    workSheet.Cell(rowIndex, colIndex).Value = cellValue.ToString(auditProps?.ExportFormat ?? "dd MMM yyyy h:mm tt");
                }
                else if (propInfo.PropertyType == typeof(TimeSpan) || propInfo.PropertyType == typeof(TimeSpan?))
                {
                    TimeSpan cellValue = Convert.ToDateTime(propInfo.GetValue(item).ToString()).TimeOfDay;// GetTimeSpanValue(propInfo, item, auditProps, objGrid);
                    workSheet.Cell(rowIndex, colIndex).Style.NumberFormat.Format = auditProps?.ExportFormat ?? "h:mm";
                    workSheet.Cell(rowIndex, colIndex).Value = cellValue.ToString(auditProps?.ExportFormat ?? "h:mm");
                }
                else
                {
                    workSheet.Cell(rowIndex, colIndex).Value = value.ToString();
                }

                workSheet.Cell(rowIndex, colIndex).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
        }

        public static Stream SaveToStream(XLWorkbook excel)
        {
            var memoryStream = new MemoryStream();
            excel.SaveAs(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }


        private static System.IO.MemoryStream ExportToPdf<T>(IPagedList<T> list, List<Core.Domain.Grid.GridColumn> Columns)
        {
            StringBuilder html = new();
            html.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>")
                .Append("<html xmlns='http://www.w3.org/1999/xhtml'>")
                .Append("<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>")
                .Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'>")
                .Append("<meta http-equiv='X-UA-Compatible' content='IE=edge'>")
                .Append("<title>SamplePdf</title></head><body>")
                .Append("<table cellpadding='5' border='1' style='border-spacing:0px;'>");

            // Add header row
            AppendHeaderRow(html, Columns);
            // Add data rows
            AppendDataRows(html, list, Columns);
            html.Append("</table></body></html>");

            //HtmlConverter htmlConverter = new(html.ToString());
            //byte[] outputByte = htmlConverter.Convert();

            //// Create and return memory stream
            //MemoryStream ms = new(outputByte)
            //{
            //    Position = 0
            //};
            //return ms;
            return new MemoryStream();
        }
        private static void AppendHeaderRow(StringBuilder html, List<Core.Domain.Grid.GridColumn> columns)
        {
            html.Append("<tr>");
            foreach (var column in columns.Where(c => !c.Name.Equals("Action", StringComparison.OrdinalIgnoreCase)))
            {
                html.Append("<th>").Append(HttpUtility.HtmlEncode(column.Name)).Append("</th>");
            }
            html.Append("</tr>");
        }
        private static void AppendDataRows<T>(StringBuilder html, IPagedList<T> list, List<Core.Domain.Grid.GridColumn> columns)
        {
            foreach (var item in list)
            {
                html.Append("<tr>");
                var propInfoList = item.GetType().GetProperties();
                foreach (var column in columns)
                {
                    if (!column.Name.Equals("Action", StringComparison.OrdinalIgnoreCase) && propInfoList.Any(p => string.Equals(p.Name, column.Data, StringComparison.OrdinalIgnoreCase)))
                    {
                        var propInfo = propInfoList.FirstOrDefault(p => string.Equals(p.Name, column.Data, StringComparison.OrdinalIgnoreCase));
                        if (propInfo != null)
                        {
                            var value = propInfo.GetValue(item);
                            html.Append("<td>").Append(HttpUtility.HtmlEncode(value?.ToString() ?? string.Empty)).Append("</td>");
                        }
                    }
                }
                html.Append("</tr>");
            }
        }
        private static Stream ExportToCsv<T>(IPagedList<T> list, List<Core.Domain.Grid.GridColumn> columns)
        {
            var propertiesToKeep = new HashSet<string>(
                columns.Select(x => x.Data),
                StringComparer.OrdinalIgnoreCase
            );

            var propertyAccessors = typeof(T).GetProperties()
                .Where(p => propertiesToKeep.Contains(p.Name))
                .ToList();

            var memoryStream = new MemoryStream();

            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                foreach (var prop in propertyAccessors)
                {
                    csvWriter.WriteField(prop.Name);
                }
                csvWriter.NextRecord();
                foreach (var item in list)
                {
                    foreach (var prop in propertyAccessors)
                    {
                        var value = prop.GetValue(item);
                        csvWriter.WriteField(value?.ToString() ?? string.Empty);
                    }
                    csvWriter.NextRecord();
                }
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        //private static System.IO.Stream ExportToCsv<T>(IPagedList<T> list, List<Core.Domain.Grid.GridColumn> Columns)
        //{

        //    List<string> propertiesToKeep = Columns.Select(x => x.Data).ToList();

        //    List<dynamic> filteredList = new();

        //    foreach (var item in list)
        //    {
        //        var itemDict = item.GetType().GetProperties();
        //        dynamic filteredItem = new ExpandoObject();
        //        var filteredDict = (IDictionary<string, object>)filteredItem;

        //        foreach (var prop in itemDict)
        //        {
        //            if (propertiesToKeep.Contains(prop.Name, StringComparer.OrdinalIgnoreCase))
        //            {
        //                filteredDict[prop.Name] = prop.GetValue(item);
        //            }
        //        }
        //        filteredList.Add(filteredItem);
        //    }

        //    var memoryStream = new MemoryStream();
        //    using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
        //    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        //    {
        //        csvWriter.WriteRecords(filteredList);
        //    }
        //    memoryStream.Position = 0;
        //    return memoryStream;
        //}

        #endregion

    }
}