using System.Diagnostics;

namespace BackendChallenge.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                httpContext.Items.Add("stopwatch", stopwatch);

                await _next(httpContext);
            }
            catch
            {
                httpContext.Response.StatusCode = 400;
            }
        }
    }
}
