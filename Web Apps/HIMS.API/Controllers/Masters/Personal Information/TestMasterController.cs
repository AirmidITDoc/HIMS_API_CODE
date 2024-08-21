using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TestMasterController : BaseController
    {
        private readonly IGenericService<MPathTestMaster> _repository;
        public TestMasterController(IGenericService<MPathTestMaster> repository)
        {
            _repository = repository;
        }



        //[HttpPost("Insert")]
        ////[Permission(PageCode = "PathTestMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(TestMasterModel obj)
        //{
        //    MPathTestMaster model = obj.MapTo<MPathTestMaster>();
        //    if (obj.TestId == 0)
        //    {
        //        model.CreatedDate = Convert.ToDateTime(obj.CreatedDate);
        //        model.TestTime = Convert.ToDateTime(obj.TestTime);
        //        model.AddedBy = CurrentUserId;
        //        await _repository.Add(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name added successfully.");
        //}


        [HttpPost("Insert")]
        //[Permission(PageCode = "PathTestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(TestMasterModel obj)
        {
            MPathTestMaster model = obj.MapTo<MPathTestMaster>();

            if (obj.TestId == 0)
            {
                model.CreatedDate = Convert.ToDateTime(obj.CreatedDate);
                model.TestTime = Convert.ToDateTime(obj.TestTime);
                model.AddedBy = CurrentUserId;

                if (obj.IsTemplateTest == 1)
                {
                    
                    MPathTemplateDetail objMPathTemplateDetail = obj.MPathTemplateDetail.MapTo<MPathTemplateDetail>();
                }
                //else if (obj.IsTemplateTest == 0)
                //{
                    
                //    MPathTestDetailMaster objMPathTestDetailMaster = obj.MPathTestDetailMaster.MapTo<MPathTestDetailMaster>();
                //}

                await _repository.Add(model, CurrentUserId, CurrentUserName);

             return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name added successfully.");
            }
            else
            {
               
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
        }







        [HttpPut("Edit/{id:int}")]
            //[Permission(PageCode = "PathTestMaster", Permission = PagePermission.Edit)]
            public async Task<ApiResponse> Edit(TestMasterModel obj)
            {
                MPathTestMaster model = obj.MapTo<MPathTestMaster>();
                if (obj.TestId == 0)
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                else
                {
                    model.CreatedDate = Convert.ToDateTime(obj.CreatedDate);
                    model.TestTime = Convert.ToDateTime(obj.TestTime);
                    await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
                }
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name updated successfully.");
            }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MPathTestMaster model = await _repository.GetById(x => x.TestId == Id);
            if ((model?.TestId ?? 0) > 0)
            {
                model.IsActive = false;
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }










    }
}
    
