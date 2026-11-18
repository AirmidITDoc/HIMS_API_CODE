using HIMS.Data.Models;
using HIMS.Core.Infrastructure;
using HIMS.Core.Domain.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace HIMS.API.Extensions
{
    public class RequestAuditMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestAuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, HIMSDbContext db)
        {
            // check global toggle
            if (global::HIMS.Core.Domain.Common.AppSettings.Settings?.AuditLoggingEnabled != true)
            {
                await _next(context);
                return;
            }

            if (context.Request.Path.StartsWithSegments("/api") &&
                context.Request.Path.Value?.Contains("/Login/", StringComparison.OrdinalIgnoreCase) == true)
            {
                await _next(context);
                return;
            }

            // only capture request/response bodies for write operations
            if (!HttpMethods.IsPost(context.Request.Method)
                && !HttpMethods.IsPut(context.Request.Method)
                && !HttpMethods.IsDelete(context.Request.Method)
                && !HttpMethods.IsPatch(context.Request.Method))
            {
                await _next(context);
                return;
            }
            try
            {
                // set ambient user from claims (if present)
                int userId = 0;
                string username = string.Empty;
                if (context.User != null && context.User.HasClaim(c => c.Type == "Id"))
                {
                    try
                    {
                        userId = HIMS.Core.Infrastructure.EncryptionUtility.DecryptText(context.User.Claims.First(c => c.Type == "Id").Value, HIMS.Core.Infrastructure.SecurityKeys.EnDeKey).ToInt();
                    }
                    catch { }
                }
                if (context.User != null && context.User.HasClaim(c => c.Type == "UserName"))
                {
                    try
                    {
                        username = HIMS.Core.Infrastructure.EncryptionUtility.DecryptText(context.User.Claims.First(c => c.Type == "UserName").Value, HIMS.Core.Infrastructure.SecurityKeys.EnDeKey);
                    }
                    catch { }
                }

                CurrentUserAccessor.Set(userId, username);

                // read request body
                context.Request.EnableBuffering();
                string requestBody = string.Empty;
                if (context.Request.ContentLength > 0 && context.Request.Body.CanRead)
                {
                    context.Request.Body.Position = 0;
                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                bool isLoginEndpoint = context.Request.Path.Value?.Contains("/Login/", StringComparison.OrdinalIgnoreCase) == true;
                if (isLoginEndpoint)
                {
                    requestBody = "[REDACTED]";
                }

                // capture response
                var originalBodyStream = context.Response.Body;
                string responseText = string.Empty;
                using var responseBody = new MemoryStream();
                try
                {
                    context.Response.Body = responseBody;
                    await _next(context);

                    responseBody.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(responseBody, Encoding.UTF8, leaveOpen: true))
                    {
                        responseText = await reader.ReadToEndAsync();
                    }

                    if (isLoginEndpoint)
                        responseText = "[REDACTED]";
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
                finally
                {
                    context.Response.Body = originalBodyStream;
                }
                var headers = context.Request.Headers
                    .Where(h => !string.Equals(h.Key, "Authorization", StringComparison.OrdinalIgnoreCase) &&
                                !string.Equals(h.Key, "Cookie", StringComparison.OrdinalIgnoreCase))
                    .ToDictionary(h => h.Key, h => h.Value.ToString());

                // create audit log entry
                var audit = new AuditLog
                {
                    CreatedOn = HIMS.Core.Infrastructure.AppTime.Now,
                    ActionId = (int)LogAction.Add,
                    ActionById = userId,
                    ActionByName = username,
                    LogSourceId = (int)LogSource.API,
                    LogTypeId = (int)LogType.Audit,
                    AdditionalInfo = "RequestAudit",
                    EntityName = context.Request.Path,
                    EntityId = 0,
                    Description = JsonConvert.SerializeObject(new
                    {
                        Method = context.Request.Method,
                        Path = context.Request.Path,
                        QueryString = context.Request.QueryString.ToString(),
                        RequestBody = requestBody,
                        ResponseBody = responseText,
                        Headers = headers
                    })
                };

                await db.AuditLogs.AddAsync(audit);
                // this will use HIMSDbContext.SaveChangesAsync override to include user info if present
                await db.SaveChangesAsync();
            }
            finally
            {
                CurrentUserAccessor.Clear();
            }
        }
    }
}
