using Asp.Versioning;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using HIMS.Api.Models.Common;
using HIMS.API.Models;
using HIMS.API.Utility;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HIMS.API.Controllers.License
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LicenseController : ControllerBase
    {
        public LicenseController() { }
        [HttpGet]
        public ApiResponse Get()
        {
            DataTable dt = new();
            using (SqlConnection con = new("server=192.168.2.200;database=AirmidTech_ProductLicense;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;TrustServerCertificate=True;"))
            {
                using SqlCommand cmd = new("SELECT * FROM HospitalMaster WHERE IsDeleted=0", con);
                using SqlDataAdapter da = new(cmd);
                con.Open();
                da.Fill(dt);
            }
            List<LicenseRequest> licenseRequests = new();
            foreach (DataRow dr in dt.Rows)
                licenseRequests.Add(new LicenseRequest
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    HospitalName = dr["HospitalName"].ToString() ?? string.Empty,
                    Address = dr["Address"].ToString() ?? string.Empty,
                    City = dr["City"].ToString() ?? string.Empty,
                    State = dr["State"].ToString() ?? string.Empty,
                    Country = dr["Country"].ToString() ?? string.Empty,
                    Code = dr["Code"].ToString() ?? string.Empty,
                    PublicKey = dr["PublicKey"].ToString() ?? string.Empty,
                    PrivateKey = dr["PrivateKey"].ToString() ?? string.Empty,
                    License = dr["License"].ToString() ?? string.Empty,
                    Gst = dr["Gst"].ToString() ?? string.Empty,
                    ContactPersonName = dr["ContactPersonName"].ToString() ?? string.Empty,
                    ContactPersonMobile = dr["ContactPersonMobile"].ToString() ?? string.Empty,
                    ExpDate = Convert.ToDateTime(dr["ExpDate"]),
                    MachineName = dr["MachineName"].ToString() ?? string.Empty,
                    Mac = dr["Mac"].ToString() ?? string.Empty,
                    IsActive = Convert.ToBoolean(dr["IsActive"])
                });
            return new ApiResponse() { Data = licenseRequests, Message = "License list", StatusCode = 200, StatusText = "Ok" };
        }
        [HttpGet("get-license/{id}")]
        public ApiResponse GetLicense(int Id)
        {
            return new ApiResponse() { Data = GetLicenseData(Id), Message = "License list", StatusCode = 200, StatusText = "Ok" };
        }
        [HttpPost]
        public ApiResponse Post([FromBody] LicenseRequest obj)
        {
            string validationMsg = ValidateData(obj.Id, obj.Code, obj.Mac);
            if (validationMsg == "Ok")
            {
                if (obj.Id == 0)
                    GenerateLicense(obj);
                SqlParameter[] para = new SqlParameter[] {
                new(){ ParameterName="@Id",Value=obj.Id,  SqlDbType=SqlDbType.BigInt },
                new(){ ParameterName="@HospitalName",Value=obj.HospitalName,  SqlDbType=SqlDbType.NVarChar,Size=250 },
                new(){ ParameterName="@Address",Value=obj.Address,  SqlDbType=SqlDbType.NVarChar,Size=-1 },
                new(){ ParameterName="@City",Value=obj.City,  SqlDbType=SqlDbType.NVarChar,Size=100 },
                new(){ ParameterName="@State",Value=obj.State,  SqlDbType=SqlDbType.NVarChar,Size=100 },
                new(){ ParameterName="@Country",Value=obj.Country,  SqlDbType=SqlDbType.NVarChar,Size=100 },
                new(){ ParameterName="@MachineName",Value=obj.MachineName,  SqlDbType=SqlDbType.NVarChar,Size=100 },
                new(){ ParameterName="@Mac",Value=obj.Mac,  SqlDbType=SqlDbType.NVarChar,Size=100 },
                new(){ ParameterName="@Code",Value=obj.Code,  SqlDbType=SqlDbType.NVarChar,Size=50 },
                new(){ ParameterName="@Gst",Value=obj.Gst,  SqlDbType=SqlDbType.NVarChar,Size=50 },
                new(){ ParameterName="@ContactPersonName",Value=obj.ContactPersonName,  SqlDbType=SqlDbType.NVarChar,Size=100 },
                new(){ ParameterName="@ContactPersonMobile",Value=obj.ContactPersonMobile,  SqlDbType=SqlDbType.NVarChar,Size=50 },
                 new(){ ParameterName="@PublicKey",Value=obj.PublicKey,  SqlDbType=SqlDbType.NVarChar,Size=-1 },
                new(){ ParameterName="@PrivateKey",Value=obj.PrivateKey,  SqlDbType=SqlDbType.NVarChar,Size=-1 },
                new(){ ParameterName="@License",Value=obj.License,  SqlDbType=SqlDbType.NVarChar,Size=-1 },
               new(){ ParameterName="@ExpDate",Value=obj.ExpDate,  SqlDbType=SqlDbType.DateTime },
                new(){ ParameterName="@CreatedById",Value=1,  SqlDbType=SqlDbType.Int },
            };

                if (obj.Id == 0)
                {
                    using SqlConnection con = new("server=192.168.2.200;database=AirmidTech_ProductLicense;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;TrustServerCertificate=True;");
                    using SqlCommand cmd = new("INSERT INTO HospitalMaster(HospitalName,Address,Code,City,State,Country,PublicKey,PrivateKey,License,Gst,ContactPersonName,ContactPersonMobile,IsActive,ExpDate,CreatedBy,CreatedDate,IsDeleted,MachineName,Mac)\r\n                        VALUES(@HospitalName,@Address,@Code,@City,@State,@Country,@PublicKey,@PrivateKey,@License,@Gst,@ContactPersonName,@ContactPersonMobile,1,@ExpDate,@CreatedById,GETDATE(),0,@MachineName,@Mac)", con);
                    con.Open();
                    cmd.Parameters.AddRange(para);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    using SqlConnection con = new("server=192.168.2.200;database=AirmidTech_ProductLicense;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;TrustServerCertificate=True;");
                    using SqlCommand cmd = new(@"UPDATE HospitalMaster SET
                        HospitalName=@HospitalName,Address=@Address,Code=@Code,City=@City,State=@State,Country=@Country,
                        Gst=@Gst,ContactPersonName=@ContactPersonName,
                        ContactPersonMobile=@ContactPersonMobile,ExpDate=@ExpDate
                            WHERE Id=@Id", con);
                    con.Open();
                    cmd.Parameters.AddRange(para);
                    cmd.ExecuteNonQuery();
                }
                return new ApiResponse() { Data = obj, Message = "License request received", StatusCode = 200, StatusText = "Ok" };
            }
            else
                return new ApiResponse() { Data = obj, Message = validationMsg, StatusCode = 400, StatusText = "Bad Request" };
        }
        [HttpPost]
        [Route("generate-new-license")]
        public ApiResponse NewLicense([FromBody] int Id)
        {
            LicenseRequest license = GetLicenseData(Id);

            GenerateLicense(license);
            SqlParameter[] para = new SqlParameter[] {
                new(){ ParameterName="@Id",Value=Id,  SqlDbType=SqlDbType.BigInt },
                new(){ ParameterName="@PublicKey",Value=license.PublicKey,  SqlDbType=SqlDbType.NVarChar,Size=-1 },
                new(){ ParameterName="@PrivateKey",Value=license.PrivateKey,  SqlDbType=SqlDbType.NVarChar,Size=-1 },
                new(){ ParameterName="@License",Value=license.License,  SqlDbType=SqlDbType.NVarChar,Size=-1 },
               };
            using SqlConnection con1 = new("server=192.168.2.200;database=AirmidTech_ProductLicense;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            using SqlCommand cmd1 = new(@"UPDATE HospitalMaster SET PublicKey=@PublicKey,PrivateKey=@PrivateKey,License=@License WHERE Id=@Id", con1);
            con1.Open();
            cmd1.Parameters.AddRange(para);
            cmd1.ExecuteNonQuery();

            return new ApiResponse() { Data = license, Message = "License request received", StatusCode = 200, StatusText = "Ok" };
        }
        [HttpPost("download-license")]
        public IActionResult GenerateLicense([FromBody] int Id)
        {
            LicenseRequest license = GetLicenseData(Id);
            var bytes = Encoding.UTF8.GetBytes(license.License);
            return File(bytes, "application/octet-stream", "license.lic");
        }
        [NonAction]
        public LicenseRequest GetLicenseData(int Id)
        {
            DataTable dt = new();
            SqlParameter[] para = new SqlParameter[] {
                new(){ ParameterName="@Id",Value=Id,  SqlDbType=SqlDbType.BigInt },
               };

            using SqlConnection con = new("server=192.168.2.200;database=AirmidTech_ProductLicense;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            using SqlCommand cmd = new("SELECT * FROM HospitalMaster WHERE IsDeleted=0 AND Id=@Id", con);
            using SqlDataAdapter da = new(cmd);
            cmd.Parameters.AddRange(para);
            con.Open();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return new LicenseRequest();
            DataRow dr = dt.Rows[0];
            return new()
            {
                Id = Convert.ToInt32(dr["Id"]),
                HospitalName = dr["HospitalName"].ToString() ?? string.Empty,
                Address = dr["Address"].ToString() ?? string.Empty,
                City = dr["City"].ToString() ?? string.Empty,
                State = dr["State"].ToString() ?? string.Empty,
                Country = dr["Country"].ToString() ?? string.Empty,
                Code = dr["Code"].ToString() ?? string.Empty,
                PublicKey = dr["PublicKey"].ToString() ?? string.Empty,
                PrivateKey = dr["PrivateKey"].ToString() ?? string.Empty,
                License = dr["License"].ToString() ?? string.Empty,
                Gst = dr["Gst"].ToString() ?? string.Empty,
                ContactPersonName = dr["ContactPersonName"].ToString() ?? string.Empty,
                ContactPersonMobile = dr["ContactPersonMobile"].ToString() ?? string.Empty,
                ExpDate = Convert.ToDateTime(dr["ExpDate"]),
                MachineName = dr["MachineName"].ToString() ?? string.Empty,
                Mac = dr["Mac"].ToString() ?? string.Empty,
                IsActive = Convert.ToBoolean(dr["IsActive"])
            };
        }
        [NonAction]
        public void GenerateLicense(LicenseRequest obj)
        {
            using var rsa = RSA.Create(2048);

            obj.PrivateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            obj.PublicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());


            var license = new LicenseModel
            {
                Customer = obj.HospitalName,
                MachineHash = LicenseSecurity.EncryptString(MachineFingerprint.Generate() + "||" + obj.ExpDate.ToString("yyyy-MM-dd")),
                // ExpiryDate = obj.ExpDate
            };

            var json = JsonSerializer.Serialize(license);

            using var rsa1 = RSA.Create();
            rsa1.ImportRSAPrivateKey(Convert.FromBase64String(obj.PrivateKey), out _);

            var signature = rsa1.SignData(
                Encoding.UTF8.GetBytes(json),
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            var licenseFile = new
            {
                Data = license,
                Signature = Convert.ToBase64String(signature)
            };

            //File.WriteAllText("license.lic",
            //    JsonSerializer.Serialize(licenseFile, new JsonSerializerOptions { WriteIndented = true }));
            obj.License = JsonSerializer.Serialize(licenseFile, new JsonSerializerOptions { WriteIndented = true });
        }
        [NonAction]
        public static string ValidateData(int Id, string Code, string Mac)
        {
            DataTable dt = new();
            using (SqlConnection con = new("server=192.168.2.200;database=AirmidTech_ProductLicense;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;TrustServerCertificate=True;"))
            {
                using SqlCommand cmd = new("SELECT Id FROM HospitalMaster WHERE Code=@Code AND IsActive=1 AND IsDeleted=0 AND Id<>@Id", con);
                using SqlDataAdapter da = new(cmd);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Code", Code);
                con.Open();
                da.Fill(dt);
            }
            if (dt.Rows.Count > 0)
                return "Hospital Code already exists.";

            if (Id == 0)
            {
                using (SqlConnection con = new("server=192.168.2.200;database=AirmidTech_ProductLicense;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;TrustServerCertificate=True;"))
                {
                    using SqlCommand cmd = new("SELECT Id FROM HospitalMaster WHERE Mac=@Mac AND IsActive=1 AND IsDeleted=0", con);
                    using SqlDataAdapter da = new(cmd);
                    cmd.Parameters.AddWithValue("@Mac", Mac);
                    con.Open();
                    da.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                    return "Hospital Mac Address already exists.";
            }
            return "Ok";
        }
    }

    public class LicenseRequest
    {
        public int Id { get; set; }
        public string HospitalName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
        public string PrivateKey { get; set; } = string.Empty;
        public string License { get; set; } = string.Empty;
        public string Gst { get; set; } = string.Empty;
        public string ContactPersonName { get; set; } = string.Empty;
        public string ContactPersonMobile { get; set; } = string.Empty;
        public DateTime ExpDate { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public string Mac { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }

}
