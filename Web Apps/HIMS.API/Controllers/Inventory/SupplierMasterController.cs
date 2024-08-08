using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    public class SupplierMasterController : BaseController
    {
        private readonly IAppointmentService _IAppointmentService;
        public SupplierMasterController(IAppointmentService repository)
        {
            _IAppointmentService = repository;
        }

        [HttpPost("AppointmentInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(AppointmentReqDto obj)
        {
            MSupplierMaster model = obj.Registration.MapTo<MSupplierMaster>();
            MAssignSupplierToStore objAssignSupplier = obj.Visit.MapTo<MAssignSupplierToStore>();
            if (obj.Registration.RegID == 0)
            {
                //model.RegDate = Convert.ToDateTime(obj.Registration.RegDate);
                //model.RegTime = Convert.ToDateTime(obj.Registration.RegTime);
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    //objVisitDetail.VisitDate = Convert.ToDateTime(obj.Visit.VisitDate);
                    //objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    //objVisitDetail.AddedBy = CurrentUserId;
                    //objVisitDetail.UpdatedBy = CurrentUserId;
                }
                //await _IAppointmentService.InsertAsyncSP(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Supplier Name  added successfully.");
        }
    }
}
