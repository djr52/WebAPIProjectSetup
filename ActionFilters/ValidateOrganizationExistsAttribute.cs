using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using System.Threading.Tasks;

namespace ActionFilters
{
    public class ValidateOrganizationExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggingManager _logger;

        public ValidateOrganizationExistsAttribute(IRepositoryManager repository, ILoggingManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var organization = _repository.Organization.GetOrganization(id, trackChanges);
            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("organization", organization);
                await next();
            }
        }

    }
}
