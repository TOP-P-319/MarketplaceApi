namespace UsersAPI.Users;

public sealed record GetMeResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string PhoneNumber { get; init; }

    /// <summary>Buyer | Seller</summary>
    public required string Role { get; init; }
    public required string Balance { get; init; }

    /// <summary>Active | Banned</summary>
    public required string Status { get; init; }
}
