using Asp.Versioning;
using DocumentFormat.OpenXml.Vml;
using HIMS.Api.Models.Common;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

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
                    Mac = dr["Mac"].ToString() ?? string.Empty
                });
            return new ApiResponse() { Data = licenseRequests, Message = "License list", StatusCode = 200, StatusText = "Ok" };
        }
        [HttpPost]
        public ApiResponse Post([FromBody]LicenseRequest obj)
        {
            string validationMsg = ValidateData(obj.Id, obj.Code, obj.Mac);
            if (validationMsg == "Ok")
            {
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
    }
}
