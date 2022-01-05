using CleanArchitecture.SharedKernel.Auth;
using CleanArchitecture.SharedKernel.Models;

namespace CleanArchitecture.Infrastructure.Security;

public class IdentityService : IIdentityService
{
    public Task<string> GetUserNameAsync(string userId)
    {
        return Task.FromResult(string.Empty);
    }

    public Task<bool> IsInRoleAsync(string userId, string role)
    {
        return Task.FromResult(true);
    }

    public Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        return Task.FromResult(true);
    }

    public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        return Task.FromResult((Result.Success(), string.Empty));
    }

    public Task<Result> DeleteUserAsync(string userId)
    {
        return Task.FromResult(Result.Success());
    }
}
