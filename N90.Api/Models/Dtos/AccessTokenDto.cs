namespace N90.Api.Models.Dtos;

public class AccessTokenDto
{
    public string Token { get; init; } = default!;

    public DateTimeOffset ExpiryTime { get; init; }
}