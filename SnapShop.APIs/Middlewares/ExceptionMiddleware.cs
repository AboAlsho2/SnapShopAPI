using SnapShop.APIs.Errors;
using System.Text.Json;

namespace SnapShop.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _log;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> log,IHostEnvironment env)
        {
            _next = next;
            _log = log;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) { 
        
            try
            {
             await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

            var response = _env.IsDevelopment() ? new ApiResponseException (ex.Message , ex.StackTrace.ToString()) : new ApiResponseException () ;

                var jsonOption = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse = JsonSerializer.Serialize (response, jsonOption);
                context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
