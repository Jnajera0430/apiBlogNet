public class AuthorizationMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<AuthorizationMiddleware> _logger;

  public AuthorizationMiddleware(RequestDelegate next, ILogger<AuthorizationMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task Invoke(HttpContext context)
  {
    await _next(context);

    var token = context.Request.Headers["Authorization"];
    _logger.LogDebug("este es el token en el middleware {token}", token.ToString());

  }
}

public static class AuthorizationMiddlewareExtension
{
  public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
  {
    return builder.UseMiddleware<AuthorizationMiddleware>();
  }
}