using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TheCalculatorAPI.Application.Common;

namespace TheCalculatorAPI.Application.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly ILogger<ValidationFilter> logger;

        public ValidationFilter(ILogger<ValidationFilter> logger) => (this.logger) = logger;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var response = CreateResponseModel(context);

                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }

        private ErrorViewModel CreateResponseModel(ActionExecutingContext context)
        {
            ErrorViewModel response = new();

            response.ErrorMsg = string.Join(
                                             Environment.NewLine,
                                             context.ModelState.Values.Where(e => e.Errors.Count > 0)
                         .SelectMany(E => E.Errors)
                         .Select(E => E.ErrorMessage )
                         .ToList());

            LogErrors(response);

            return response;
        }

        private bool LogErrors(ErrorViewModel response)
        {
            this.logger.LogError($"Validation error {DateTime.Now} {response.ErrorMsg}");
            return true;
        }
    }
}
