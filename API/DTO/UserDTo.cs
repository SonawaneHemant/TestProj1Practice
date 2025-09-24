using System;

namespace API.DTO;

public class UserDTo
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public string? ImageURl { get; set; }
    public required string Token { get; set; }
}
