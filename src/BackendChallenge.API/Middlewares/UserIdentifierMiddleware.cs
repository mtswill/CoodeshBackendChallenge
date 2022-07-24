using BackendChallenge.Core.Helpers;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Core.Interfaces.Users;
using System.Security.Claims;
using System.Text.Json;

namespace BackendChallenge.API.Middlewares
{
    public class UserIdentifierMiddleware
    {
        private static IEnumerable<string> _allowAnonymousPaths = new string[] { "/auth/signup", "/auth/signin", "/" };
        private readonly RequestDelegate _next;

        public UserIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IUser user, IUserRepository userRepository)
        {
            var isAllowed = false;

            foreach (var path in _allowAnonymousPaths)
            {
                if (httpContext.Request.Path.Value!.ToLower() == path)
                {
                    isAllowed = true;
                    break;
                }
            }

            if (isAllowed)
            {
                await _next(httpContext);
                return;
            }

            var userId = Guid.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value!);
            var cacheKey = $"user_{userId}";

            var cached = CacheHelper.GetCache(cacheKey);

            Core.Entities.User? requestUser = null;

            if (cached is not null)
                requestUser = JsonSerializer.Deserialize<Core.Entities.User>(cached);
            else
                requestUser = await userRepository.GetUserByIdAsync(userId);

            if (requestUser is null)
            {
                httpContext.Response.StatusCode = 400;
                return;
            }

            if (cached is null)
                CacheHelper.SetCache(cacheKey, requestUser);

            user.RequestUser = requestUser;
            user.RequestUser!.Password = "";

            await _next(httpContext);
        }
    }
}
