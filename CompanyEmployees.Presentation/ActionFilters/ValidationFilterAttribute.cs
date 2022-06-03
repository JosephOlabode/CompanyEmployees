using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyEmployees.ActionFilters
{
	public class ValidationFilterAttribute: IActionFilter
	{
		public ValidationFilterAttribute()
		{
		}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var action = context.RouteData.Values["action"];
            //var controller = context.RouteData.Values["controller"];

            //var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            //if(param is null)
            //{
            //    context.Result = new BadRequestObjectResult($"Object is null. Controller:{ controller }, action: { action}");
            //    return;
            //}

            //if (!context.ModelState.IsValid)
            //    context.Result = new UnprocessableEntityObjectResult(context.ModelState);

            var acceptHeaderPresent = context.HttpContext
                     .Request.Headers.ContainsKey("Accept");

            if (!acceptHeaderPresent)
            {
                context.Result = new BadRequestObjectResult($"Accept header is missing.");
                return;
            }

            var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue ? outMediaType))
            {
                context.Result = new BadRequestObjectResult($"Media type not present. Please add Accept header with the required media type.");
                return;
            }
            context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);

        }
    }
}

