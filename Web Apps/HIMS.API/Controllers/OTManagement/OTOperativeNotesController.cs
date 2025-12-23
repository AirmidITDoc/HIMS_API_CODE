using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.OTManagment;
using HIMS.Api.Models.Common;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OTManagement;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;

namespace HIMS.API.Controllers.OTManagement
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OTOperativeNotesController : BaseController
    {
        private readonly IOTOperativeNotes _IOTOperativeNotes;
        private readonly IGenericService<TOtOperativeNote> _repository;
        public OTOperativeNotesController(IOTOperativeNotes repository, IGenericService<TOtOperativeNote> repository1)
        {
            _IOTOperativeNotes = repository;
            _repository = repository1;
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "TOtOperativeNote", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OTOperativeNotesModel obj)
        {
            TOtOperativeNote model = obj.MapTo<TOtOperativeNote>();
            if (obj.OperativeNotesId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.Createdby = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IOTOperativeNotes.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "TOtOperativeNote", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTOperativeNotesModel obj)
        {
            TOtOperativeNote model = obj.MapTo<TOtOperativeNote>();
            if (obj.OperativeNotesId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;

                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "Createdby", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

        [HttpGet("{id?}")]
        //[Permission(PageCode = "TOtOperativeNote", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.OtreservationId == id);
            return data.ToSingleResponse<TOtOperativeNote, OTOperativeNotesModel>("AreaMaster");
        }
    }
}
