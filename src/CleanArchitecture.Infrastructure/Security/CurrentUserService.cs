using System.Security.Claims;
using CleanArchitecture.SharedKernel.Auth;
using Dawn;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Security;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = Guard.Argument(httpContextAccessor, nameof(httpContextAccessor)).NotNull().Value;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
