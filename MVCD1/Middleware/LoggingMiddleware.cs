namespace MVCD2.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context) {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while processing the request");
                context.Items["Error Message"]=ex.Message;
                context.Items["Error Details"] = ex.ToString();
                context.Response.Redirect("/Home/Error");

            }
            if (context.Response.StatusCode == 404)
            {
                _logger.LogWarning("page not found" + context.Request.Path);
                context.Items["Error Message"] = "page doesnot exist";
                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
