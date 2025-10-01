namespace UserManagementAPI.Middleware;
public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthenticationMiddleware> _logger;

    public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip authentication for Swagger and development
        if (context.Request.Path.StartsWithSegments("/swagger") ||
            context.Request.Path.StartsWithSegments("/weatherforecast"))
        {
            await _next(context);
            return;
        }

        // Simple token-based authentication (for demo purposes)
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authorization header is required");
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();
        if (!authHeader.StartsWith("Bearer "))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid authorization format. Use 'Bearer {token}'");
            return;
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();

        // Validate token (in real application, validate against database or identity provider)
        if (string.IsNullOrEmpty(token) || token != "valid-token-123")
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid or expired token");
            return;
        }

        _logger.LogInformation($"User authenticated for request: {context.Request.Method} {context.Request.Path}");
        await _next(context);
    }
}