namespace UsersAPI.Auth;

public sealed record LoginResponse
{
    public required string Token { get; init; }
}