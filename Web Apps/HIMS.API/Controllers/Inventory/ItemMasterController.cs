using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.Inventory
{
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
                    model.Addedby = CurrentUserId;
                    await _repository.Add(model, CurrentUserId, CurrentUserName);
                }
                else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item added successfully.");
            }
        }
}
