using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Shop.WebApi.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        private readonly IHostingEnvironment _env;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IHostingEnvironment env)
        {
            this._logger = logger;
            this._env = env;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.HttpContext.RequestAborted.IsCancellationRequested)
                return;

            string method = context?.HttpContext?.Request?.Method;
            string path = context?.HttpContext?.Request?.Path;
            string queryString = string.Empty;

            _logger.LogError($"Methode:{method}. Path:{path}. Query:{queryString}", context?.Exception);

            if (_env.IsDevelopment())
                context.Result = new OkObjectResult(JsonConvert.SerializeObject(context?.Exception, Formatting.Indented)); 
            else
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);

            context.ExceptionHandled = true;
            await base.OnExceptionAsync(context);
        }
    }
}
