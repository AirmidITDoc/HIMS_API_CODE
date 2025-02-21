using HIMS.Api.Controllers;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    public class RoleController : BaseController
    { 
        private readonly IRoleService _IroleService;
        public RoleController(IRoleService repository)
        {
            _IroleService = repository;
        }


        //[HttpGet]
        //[Route("get-permissions")]
        //public IActionResult GetPermission(int RoleId)
        //{
        //    return Ok(_IroleService.GetPermisison(RoleId));
        //}
        //[HttpPost]
        //[Route("save-permission")]
        //public IActionResult PostPermission(List<PermissionModel> obj)
        //{
        //    _IroleService.SavePermission(obj);
        //    return Ok();
        //}
    }
}
