using System.Security.Claims;
using System.Text;

namespace D38_WebApiDemo.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string autoHeader = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(autoHeader))
            {
                await _next(context);
                return;
            }


            if (autoHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var token = autoHeader.Substring(6).Trim();
                    var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var credentials = credentialString.Split(':');

                    
                    if (credentials.Length == 2 && credentials[0] == "alperen" && credentials[1] == "12345")
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, credentials[0]),
                            new Claim(ClaimTypes.Role, "Admin"),
                            new Claim(ClaimTypes.Role, "Editor")
                        };

                        var identity = new ClaimsIdentity(claims, "Basic");
                        context.User = new ClaimsPrincipal(identity);

                        await _next(context);
                        return;
                    }
                }
                catch
                {
                    context.Response.StatusCode = 500;
                    return;
                }
            }
            context.Response.StatusCode = 401;
        }
    }
}
