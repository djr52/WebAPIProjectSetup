using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using System.Linq;
namespace ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly ILoggingManager _logger;
        public ValidationFilterAttribute(ILoggingManager logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context) 
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (param == null)
            {
                _logger.LogError($"Object sent from client is null. Controller: {controller}, Action: {action}");
                context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, Action: {action}");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the object. Controller: {controller}, Action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }


        }
        public void OnActionExecuted(ActionExecutedContext context) { }

        
    }
}
