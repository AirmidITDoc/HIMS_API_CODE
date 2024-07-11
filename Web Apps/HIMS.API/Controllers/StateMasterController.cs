using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models;

namespace HIMS.API.Controllers
{
    public class StateMasterController : Controller
    {
        private readonly IGenericService<MStateMaster> _repository;
        public StateMasterController(IGenericService<MStateMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MStateMaster> StateMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(StateMasterList.ToGridResponse(objGrid, "State List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.StateId == id);
            return data.ToSingleResponse<MStateMaster, StateMasterModel>("StateMaster");
        }


       
    }
}
