using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using HIMS.API.Models.Common;
using HIMS.Core.Domain.Logging;
using HIMS.Api.Models.Common;

namespace HIMS.API.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microsoft.IO.RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly IConfiguration Configuration;
        public ExceptionHandlingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _recyclableMemoryStreamManager = new Microsoft.IO.RecyclableMemoryStreamManager();
            Configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            string IsAllowLog = Configuration["RequestLog:IsAllow"];
            RequestLog log = new();
            try
            {
                if ((IsAllowLog ?? "false").ToLower() == "true")
                {
                    log = await LogRequest(context, log);
                    log = await LogResponse(context, log);
                }
                else
                {
                    context.Request.EnableBuffering();
                    await _next.Invoke(context);
                }
            }
            catch (Exception ex)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                log.ResponseCode = "500";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string param = "";
                if (context.Request.Method.ToLower() == "get")
                {
                    param = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : "";
                }
                else
                {
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                    using (StreamReader stream = new(context.Request.Body))
                    {
                        param = stream.ReadToEnd();
                    }
                    context.Request.Body.Dispose();
                }
                string msg = ex == null ? "{Params:" + param + "}" : "{ Params: " + param + ",DetailError: " + (ex.InnerException != null ? (ex.InnerException.ToString().ToLower() == "undefined" ? ex.Message : ex.InnerException) : ex?.Message ?? "") + "}";
                string errorJson = JsonSerializer.Serialize(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Error. Please try again after some time.", new { ApiUrl = context.Request.Path.Value }));

                await response.WriteAsync(errorJson);
            }
        }
        private async Task<RequestLog> LogRequest(HttpContext context, RequestLog Log)
        {
            context.Request.EnableBuffering();
            await using MemoryStream requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            Log.Path = context.Request.Path;
            Log.Method = context.Request.Method;
            Log.QueryString = context.Request.QueryString.ToString();
            Log.RequestedOn = DateTime.UtcNow;
            if (context.Request.Method != "GET")
            {
                Log.Payload = ReadStreamInChunks(requestStream);
                if (context.Request.Path.ToString().ToLower().EndsWith("/authenticate"))
                {
                    var pass = Log.Payload.Split(new string[] { ",\"Password\":", "," }, StringSplitOptions.None);
                    if (pass.Length > 1)
                        Log.Payload = Log.Payload.Replace(pass[1], "\"DummayPassword\"");
                }
            }
            context.Request.Body.Position = 0;
            return Log;
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using StringWriter textWriter = new();
            using StreamReader reader = new(stream);
            char[] readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                0,
                                                readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
        private async Task<RequestLog> LogResponse(HttpContext context, RequestLog Log)
        {
            Stream originalBodyStream = context.Response.Body;
            await using MemoryStream responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            Log.Response = text;
            Log.RespondedOn = DateTime.UtcNow;
            Log.ResponseCode = context.Response.StatusCode.ToString();
            await responseBody.CopyToAsync(originalBodyStream);
            return Log;
        }
    }
}
