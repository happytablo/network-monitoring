namespace a_webapi.DTOs.Auth;

public record LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
}