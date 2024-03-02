using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using HIMS.API.Models.Common;
using HIMS.Api.Models.Common;

namespace HIMS.API.Extensions
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //check that client does not use any invalid parameter
            //but just those that are required by WS methods
            var parameters = context.ActionDescriptor.Parameters.ToList();
            var queryParameters = context.HttpContext.Request.Query.Keys.ToList();

            if (queryParameters.Count > 0 && queryParameters.Any(queryParameter => !parameters.Any(p => p.Name.Trim().ToLower() == queryParameter.Trim().ToLower())))
            {
                context.Result = new BadRequestObjectResult(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Bad Request."));
            }
            else
            {
                if (!context.ModelState.IsValid && !IsAttributeExist(context))
                {
                    var valid = context.ModelState.Values.SelectMany(x => x.Errors).Where(x => x.ErrorMessage != "The value '' is invalid.").Count();
                    var EmptyVal = context.ModelState.Values.SelectMany(x => x.Errors).Where(x => x.ErrorMessage == "The value '' is invalid.").Count();
                    if (EmptyVal == 0 || valid > 0)
                    {
                        string _message = string.Join(",", context.ModelState.Values
                                                           .SelectMany(x => x.Errors)
                                                           .Select(x => x.ErrorMessage).Distinct());

                        context.Result = new BadRequestObjectResult(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, _message));
                    }
                }
            }
        }
        private static bool IsAttributeExist(ActionExecutingContext context)
        {
            return (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(DontValidateAttribute), false).Any();
        }
    }
}