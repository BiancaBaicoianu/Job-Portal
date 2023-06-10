using JobPortal.API.Helpers.JwtUtils;
using JobPortal.API.Services;

namespace JobPortal.API.Helpers.MiddleWare
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IAuthenticationService authenticationService, IJwtUtils jwtUtils)
        {
            // in httpContext avem toata informatia request-ului nostru
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            // var secret = _configuration["AppSettings:Secret"];
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                context.Items["User"] = authenticationService.GetById(userId);
            }
            await _next(context);
        }

       
        
    }
}
