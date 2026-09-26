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

                // capture response
                var originalBodyStream = context.Response.Body;
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseText = string.Empty;
                using (var reader = new StreamReader(context.Response.Body, Encoding.UTF8, leaveOpen: true))
                {
                    responseText = await reader.ReadToEndAsync();
                }
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                // write response back to original stream
                await responseBody.CopyToAsync(originalBodyStream);

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
                        Headers = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
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
