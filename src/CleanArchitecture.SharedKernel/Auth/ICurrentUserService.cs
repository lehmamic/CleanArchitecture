namespace CleanArchitecture.SharedKernel.Auth;

public interface ICurrentUserService
{
    string? UserId { get; }
}
