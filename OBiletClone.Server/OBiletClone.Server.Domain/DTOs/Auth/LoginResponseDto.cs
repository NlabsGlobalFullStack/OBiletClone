namespace OBiletClone.Server.Domain.DTOs.Auth;
public sealed record LoginResponseDto(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpires);