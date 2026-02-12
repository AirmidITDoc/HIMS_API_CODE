using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Common
{
    public static class AppSettings
    {
        public static AppSettingsDto Settings { get; private set; }

        public static void Initialize(IConfiguration configuration)
        {
            Settings = configuration.Get<AppSettingsDto>();
        }
    }
    public class AppSettingsDto
    {
        public string CONNECTION_STRING { get; set; }
        public string CorsAllowUrls { get; set; }
        public string SwaggerUrl { get; set; }
        public string BaseUrl { get; set; }
        public string PdfFontPath { get; set; }
        public string StorageBaseUrl { get; set; }
        public string PdfTemplatePath { get; set; }

        public AuthenticationSettings AuthenticationSettings { get; set; }
        public AESSecurityKeys AESSecurityKeys { get; set; }
        public Licence Licence { get; set; }
        public MPesa MPesa { get; set; }
    }
    public class AuthenticationSettings
    {
        public string SecretKey { get; set; }
    }
    public class AESSecurityKeys
    {
        public string key { get; set; }
        public string iv { get; set; }
    }
    public class Licence
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
    public class MPesa
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string ShortCode { get; set; }
        public string AccessTokenUrl { get; set; }
        public string ConfirmationUrl { get; set; }
        public string ValidationUrl { get; set; }
        public string BusinessShortCode { get; set; }
        public string PassKey { get; set; }
        public string RegisterUrl { get; set; }
        public string StkPushUrl { get; set; }
        public string TransactionType { get; set; }
        public string ResponseType { get; set; }
    }

}
