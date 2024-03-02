using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace HIMS.Api.Models.Common
{
    public static class ApiResponseHelper
    {
        public static ApiResponse GenerateResponse(ApiStatusCode StatusCode, string Message, object Data = null, object ExtraData = null, string StatusText = "")
        {
            return  new ApiResponse() { StatusCode = (int)StatusCode, StatusText = StatusText == "" ? StatusCode.ToString() : StatusText, Message = Message, Data = Data, ExtraData = ExtraData };
        }
        
    }
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string StatusText { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object ExtraData { get; set; }
    }
    public enum ApiStatusCode
    {
        Status100Continue = 100,
        Status412PreconditionFailed = 412,
        Status413RequestEntityTooLarge = 413,
        Status414RequestUriTooLong = 414,
        Status415UnsupportedMediaType = 415,
        Status416RequestedRangeNotSatisfiable = 416,
        Status417ExpectationFailed = 417,
        Status418ImATeapot = 418,
        Status419AuthenticationTimeout = 419,
        Status421MisdirectedRequest = 421,
        Status422UnprocessableEntity = 422,
        Status423Locked = 423,
        Status424FailedDependency = 424,
        Status426UpgradeRequired = 426,
        Status428PreconditionRequired = 428,
        Status429TooManyRequests = 429,
        Status431RequestHeaderFieldsTooLarge = 431,
        Status451UnavailableForLegalReasons = 451,
        Status500InternalServerError = 500,
        Status501NotImplemented = 501,
        Status502BadGateway = 502,
        Status503ServiceUnavailable = 503,
        Status504GatewayTimeout = 504,
        Status505HttpVersionNotsupported = 505,
        Status506VariantAlsoNegotiates = 506,
        Status507InsufficientStorage = 507,
        Status508LoopDetected = 508,
        Status411LengthRequired = 411,
        Status510NotExtended = 510,
        Status410Gone = 410,
        Status408RequestTimeout = 408,
        Status101SwitchingProtocols = 101,
        Status102Processing = 102,
        Status200OK = 200,
        Status201Created = 201,
        Status202Accepted = 202,
        Status203NonAuthoritative = 203,
        Status204NoContent = 204,
        Status205ResetContent = 205,
        Status206PartialContent = 206,
        Status207MultiStatus = 207,
        Status208AlreadyReported = 208,
        Status226IMUsed = 226,
        Status300MultipleChoices = 300,
        Status301MovedPermanently = 301,
        Status302Found = 302,
        Status303SeeOther = 303,
        Status304NotModified = 304,
        Status305UseProxy = 305,
        Status306SwitchProxy = 306,
        Status307TemporaryRedirect = 307,
        Status308PermanentRedirect = 308,
        Status400BadRequest = 400,
        Status401Unauthorized = 401,
        Status402PaymentRequired = 402,
        Status403Forbidden = 403,
        Status404NotFound = 404,
        Status405MethodNotAllowed = 405,
        Status406NotAcceptable = 406,
        Status407ProxyAuthenticationRequired = 407,
        Status409Conflict = 409,
        Status511NetworkAuthenticationRequired = 511,
        StatusCutomError = 6
    }
}
