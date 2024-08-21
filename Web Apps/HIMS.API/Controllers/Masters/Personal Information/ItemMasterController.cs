using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
        [ApiController]
        [ApiVersion("1")]
        public class ItemMasterController : BaseController
        {
            private readonly IGenericService<MItemMaster> _repository;
            public ItemMasterController(IGenericService<MItemMaster> repository)
            {
                _repository = repository;
            }

            [HttpPost("Insert")]
            //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
            public async Task<ApiResponse> Insert(ItemMasterModel obj)
            {
                MItemMaster model = obj.MapTo<MItemMaster>();
                if (obj.ItemId == 0)
                {
                model.CreatedDate = Convert.ToDateTime(obj.CreatedDate);
                //model.ItemTime = Convert.ToDateTime(obj.ItemTime);
                model.Addedby = CurrentUserId;
                    await _repository.Add(model, CurrentUserId, CurrentUserName);
                }
                else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item added successfully.");
            }
            //Edit API
            [HttpPut("{id:int}")]
            //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Edit)]
            public async Task<ApiResponse> Edit(ItemMasterModel obj)
            {
                MItemMaster model = obj.MapTo<MItemMaster>();
                model.Isdeleted = true;
                if (obj.ItemId == 0)
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                else
                {
                    model.UpDatedBy = CurrentUserId;
                    model.IsUpdatedBy = DateTime.Now;
                    await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
                }
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Name updated successfully.");
            }
            //Delete API
            [HttpDelete]
            //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Delete)]
            public async Task<ApiResponse> Delete(int Id)
            {
                MItemMaster model = await _repository.GetById(x => x.ItemId == Id);
                if ((model?.ItemId ?? 0) > 0)
                {
                    model.Isdeleted = false;
                    model.UpDatedBy = CurrentUserId;
                    model.IsUpdatedBy = DateTime.Now;
                    await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Name deleted successfully.");
                }
                else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
        }
    }



