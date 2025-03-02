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

namespace HIMS.API.Extensions
{
    public static class CommonExtensions
    {
        public static HttpContext HttpContextAccessor => new HttpContextAccessor().HttpContext;
        private static readonly DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static object ToGridResponse<T>(this IPagedList<T> GridData, GridRequestModel objGrid, string FileName)
        {
            try
            {
                return new ApiResponse { StatusCode = 200, Data = GridData.ToGrid() };
                // return objGrid.ExportType == 0 ? new ApiResponse { StatusCode = 200, Data = GridData.ToGrid() } : GridData.ExportToGrid(objGrid, FileName);
            }
            catch (Exception)
            {
                return new object();
            }
        }

        public static object ToGridSaleResponse<T>(this IPagedList<T> GridData, GridRequestModel objGrid, string FileName, object ExtraData)
        {
            try
            {
                return new ApiResponse { StatusCode = 200, Data = GridData.ToGrid(), ExtraData = ExtraData };
                //return objGrid.ExportType == 0 ? new ApiResponse { StatusCode = 200, Data = GridData.ToGrid(), ExtraData = ExtraData } : GridData.ExportToGrid(objGrid, FileName);
            }
            catch (Exception)
            {
                return new object();
            }
        }
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
        public static object ToGridResponse<T, TResult>(this IPagedList<T> GridData, GridRequestModel objGrid, string FileName, Func<T, TResult> selector)
        {
            try
            {
                return new ApiResponse { StatusCode = 200, Data = GridData.ToGrid(selector) };
                //return objGrid.ExportType == 0 ? new ApiResponse { StatusCode = 200, Data = GridData.ToGrid(selector) } : GridData.ExportToGrid(objGrid, FileName);
            }
            catch (Exception)
            {
                return new object();
            }
        }
        public static GridResponseModel ToGrid<T, TResult>(this IPagedList<T> GridData, Func<T, TResult> selector)
        {
            try
            {
                return new GridResponseModel() { Data = GridData.Select(selector), RecordsFiltered = GridData.TotalCount, RecordsTotal = GridData.TotalCount, PageIndex = GridData.PageIndex };
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
            List<SelectListItem> items = new List<SelectListItem>();
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


    }
}