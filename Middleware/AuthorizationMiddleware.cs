
namespace Simple_Bank_Application.Middleware
{
}
public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint != null)
        {
            var requiresAuth = endpoint.Metadata.GetMetadata<RequiresAuthAttribute>();
            var adminAuth = endpoint.Metadata.GetMetadata<AdminAuthAttribute>();

            var username = context.Session.GetString("username");
            var role = context.Session.GetString("role");

            if(adminAuth != null)
            {
                if(string.IsNullOrEmpty(role) || role != "admin")
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Access Denied: Admin Only");
                }
            }

            if (requiresAuth != null)
            {

                if (string.IsNullOrEmpty(username))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }
        }

        await _next(context);
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class RequiresAuthAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class AdminAuthAttribute : Attribute { }

