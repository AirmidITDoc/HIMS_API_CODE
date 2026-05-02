using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Common;
using HIMS.Core.Domain.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HIMS.API.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microsoft.IO.RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        // Static SemaphoreSlim ensures only one thread writes to the file at a time
        // across all requests — without throwing if busy
        private static readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);

        private readonly string _logDirectory;
        private readonly string _logFilePrefix;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new Microsoft.IO.RecyclableMemoryStreamManager();

            // Read from appsettings.json or fallback to defaults
            _logDirectory = configuration["ExceptionLogging:Directory"] ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            _logFilePrefix = configuration["ExceptionLogging:FilePrefix"] ?? "exception";
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory); // Ensure the directory exists
        }

        public async Task Invoke(HttpContext context)
        {
            string IsAllowLog = "false";
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
                // --- 1. Read request body for logging ---
                string param = string.Empty;
                try
                {
                    if (context.Request.Method.ToLower() != "get")
                    {
                        context.Request.Body.Seek(0, SeekOrigin.Begin);
                        using StreamReader stream = new(context.Request.Body, leaveOpen: true);
                        param = await stream.ReadToEndAsync();
                    }
                    else
                    {
                        param = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : string.Empty;
                    }
                }
                catch
                {
                    param = "[Could not read request body]";
                }

                // --- 2. Log exception to file (non-blocking, never throws) ---
                _ = LogExceptionToFileAsync(ex, context, param); // fire-and-forget

                // --- 3. Send JSON error response ---
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                log.ResponseCode = "500";

                string errorJson = JsonSerializer.Serialize(new
                {
                    Error = ex.Message,
                    ApiUrl = context.Request.Path.Value
                });

                await response.WriteAsync(errorJson);
            }
        }

        /// <summary>
        /// Writes exception details to a daily log file.
        /// Uses SemaphoreSlim.WaitAsync with a timeout so it never blocks or throws
        /// when the file is busy — it simply drops the entry (or retries briefly).
        /// </summary>
        private async Task LogExceptionToFileAsync(Exception ex, HttpContext context, string requestParam)
        {
            try
            {
                string logEntry = BuildLogEntry(ex, context, requestParam);

                // Daily rolling file: exception_2025-01-31.log
                string fileName = $"{_logFilePrefix}_{DateTime.UtcNow:yyyy-MM-dd}.log";
                string filePath = Path.Combine(_logDirectory, fileName);

                // Try to acquire the lock — wait max 2 seconds, then skip (never throw)
                bool lockAcquired = await _fileLock.WaitAsync(TimeSpan.FromSeconds(2));
                if (!lockAcquired)
                {
                    // File is busy — log a warning via ILogger instead of throwing
                    _logger.LogWarning("ExceptionMiddleware: Log file busy, skipping file write for exception: {Message}", ex.Message);
                    return;
                }

                try
                {
                    // FileShare.Read allows other processes to read while we write
                    await using FileStream fs = new(filePath, FileMode.Append, FileAccess.Write, FileShare.Read, bufferSize: 4096, useAsync: true);
                    await using StreamWriter writer = new(fs, Encoding.UTF8);
                    await writer.WriteAsync(logEntry);
                    await writer.FlushAsync();
                }
                finally
                {
                    _fileLock.Release(); // Always release, even on error
                }
            }
            catch (Exception fileEx)
            {
                // Last resort — never let logging crash the app
                _logger.LogError(fileEx, "ExceptionMiddleware: Failed to write exception log to file.");
            }
        }

        /// <summary>
        /// Builds a structured, readable log entry string.
        /// </summary>
        private static string BuildLogEntry(Exception ex, HttpContext context, string requestParam)
        {
            StringBuilder sb = new();
            sb.AppendLine("=".PadRight(80, '='));
            sb.AppendLine($"  Timestamp (UTC) : {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}");
            sb.AppendLine($"  Request URL     : {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
            sb.AppendLine($"  Client IP       : {context.Connection.RemoteIpAddress}");
            sb.AppendLine($"  User            : {context.User?.Identity?.Name ?? "Anonymous"}");
            sb.AppendLine($"  Request Param   : {(string.IsNullOrWhiteSpace(requestParam) ? "N/A" : requestParam)}");
            sb.AppendLine("-".PadRight(80, '-'));
            sb.AppendLine($"  Exception Type  : {ex.GetType().FullName}");
            sb.AppendLine($"  Message         : {ex.Message}");

            // Log inner exceptions recursively
            Exception inner = ex.InnerException;
            int depth = 1;
            while (inner != null)
            {
                sb.AppendLine($"  Inner ({depth})       : [{inner.GetType().Name}] {inner.Message}");
                inner = inner.InnerException;
                depth++;
            }

            sb.AppendLine("-".PadRight(80, '-'));
            sb.AppendLine($"  Stack Trace:");
            sb.AppendLine(ex.StackTrace ?? "  N/A");
            sb.AppendLine("=".PadRight(80, '='));
            sb.AppendLine();

            return sb.ToString();
        }

        // ── Existing methods unchanged ────────────────────────────────────────────

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
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
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