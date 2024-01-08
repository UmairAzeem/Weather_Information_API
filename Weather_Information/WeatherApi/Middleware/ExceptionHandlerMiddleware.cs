using BusinessLogic.Models;
using System.Diagnostics;
using WeatherApi.Models;

namespace WeatherApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Invoke the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the exception
                var routeData = context.GetRouteData();
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];

                var stackTrace = new StackTrace(ex, true);
                var frame = stackTrace.GetFrame(0);
                string err_detail = stackTrace.ToString();

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                _logger.LogError(ex, "An exception occurred.");

                if (_env.IsDevelopment())
                {
                    Console.WriteLine($"--------------------------------- Exception --------------------------------- \nStatus Code: {context.Response.StatusCode} \nController: {controllerName} \nMethod: {actionName} \nError_Details: {err_detail} \nMessage: {ex.Message} \n--------------------------------------------------------------");

                    await HandleExceptionAsync(context, ex);
                }
                else
                {
                    // Save in Database or file to track exception in Stagging or Production
                    await HandleExceptionAsync(context, ex);
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Prepare your custom response object here
            var res = new ApiResponse();
            {
                res.Data = null;
                res.Success = false;
                res.Message = $"{StatusCodes.Status500InternalServerError}: Error ";
                // Add more properties if needed
            };

            // Serialize the response object and send it as the response
            return context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(res));
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
