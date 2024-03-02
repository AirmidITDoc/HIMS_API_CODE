using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using HIMS.Data.Models;
using System;
using Microsoft.AspNetCore.Http.Features;
using HIMS.API.Infrastructure;
using Microsoft.OpenApi.Models;
using HIMS.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Asp.Versioning;
using System.Runtime.Intrinsics;
using HIMS.Core.Utilities;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

// Add services to the container.
ConfigurationManager Configuration = builder.Configuration;
ConfigurationHelper.Initialize(Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = long.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
//Entity Framework  
builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddDbContextPool<SSDB_JSS_WEB_25SepContext>((provider, options) =>
{
    options.UseSqlServer(Configuration.GetValue<string>("CONNECTION_STRING"));
    options.UseInternalServiceProvider(provider);
});
DependencyRegistrar.Register(builder.Services);
builder.Services.AddMvc(opt =>
{
    opt.EnableEndpointRouting = false;
    opt.Filters.Add(typeof(ValidateModelStateAttribute));
}).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

MapperConfiguration mapperConfiguration = new(configure => configure.AddProfile<ApplicationMappingProfile>());
mapperConfiguration.CreateMapper().InitializeMapper();

builder.Services.AddMvc().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
    jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new HeaderApiVersionReader("api-version");
});

//Configure JWT Token Authentication
AuthenticationSettings config = builder.Configuration.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();
byte[] secretKey = Encoding.ASCII.GetBytes(config.SecretKey);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.Project", Version = "v1" });
    c.MapType<DateTime>(() => new OpenApiSchema { Format = "dd/MMM/yyyy hh:mm tt", Type = "DateTime" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.\r\n\r\nExample: \"12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()

                    }
                });
});

string[] CorsAllowUrls = Configuration["CorsAllowUrls"].Split(',');
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corspolicy",
                      builder =>
                      {
                          builder
                          .WithOrigins(CorsAllowUrls).SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")//.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("Content-Disposition");
                      });
    /*options.AddPolicy(name: "dev_corspolicy",
                      builder =>
                      {
                          builder
                          .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")//.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });*/
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint(Configuration["SwaggerUrl"], "API v1"));

app.UseAuthentication();

app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
