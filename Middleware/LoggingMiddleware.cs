namespace UserManagementAPI.Middleware;
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;

        // Log request
        _logger.LogInformation($"HTTP {context.Request.Method} {context.Request.Path} started at {startTime}");

        // Copy original response body stream
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        var endTime = DateTime.UtcNow;
        var elapsed = endTime - startTime;

        // Log response
        _logger.LogInformation($"HTTP {context.Request.Method} {context.Request.Path} completed with status {context.Response.StatusCode} in {elapsed.TotalMilliseconds}ms");

        // Copy the response body to the original stream
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }
}