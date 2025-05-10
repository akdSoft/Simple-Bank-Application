

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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

            if (requiresAuth != null)
            {
                var username = context.Session.GetString("username");

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

//[AttributeUsage(AttributeTargets.Method)]
public class RequiresAuthAttribute : Attribute { }

