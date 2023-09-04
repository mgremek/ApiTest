using Microsoft.AspNetCore.Http;

namespace Api_Cats.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               _logger.LogInformation($"HTTP request information:\n" +
               $"\tMethod: {context.Request.Method}\n" +
               $"\tPath: {context.Request.Path}\n" +
               $"\tQueryString: {context.Request.QueryString}\n" +
               $"\tHeaders: {FormatHeaders(context.Request.Headers)}\n" +
               $"\tSchema: {context.Request.Scheme}\n" +
               $"\tHost: {context.Request.Host}\n" +
               $"\tBody: {await ReadBodyFromRequest(context.Request)}");

                await next.Invoke(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
        private static string FormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));
        private static async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            // Ensure the request's body can be read multiple times (for the next middlewares in the pipeline).  
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            // Reset the request's body stream position for next middleware in the pipeline.  
            request.Body.Position = 0;
            return requestBody;
        }
    }
}
