public class TimeMiddleware
{
  readonly RequestDelegate next;

  public TimeMiddleware(RequestDelegate nextRequest)
  {
    this.next = nextRequest;
  }

  public async Task Invoke(HttpContext context)
  {
    await next(context);

    if (context.Request.Query.Any(q => q.Key == "time"))
    {
      await context.Response.WriteAsJsonAsync(DateTime.Now.ToShortTimeString());
    }
  }

}
public static class TimeMiddlewareExtension
{
  public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
  {
    return builder.UseMiddleware<TimeMiddleware>();
  }
}