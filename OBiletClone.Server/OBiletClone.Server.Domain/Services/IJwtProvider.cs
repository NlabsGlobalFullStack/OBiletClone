using OBiletClone.Server.Domain.DTOs.Auth;
using OBiletClone.Server.Domain.Entities;

namespace OBiletClone.Server.Domain.Services;
public interface IJwtProvider
{
    Task<LoginResponseDto> CreateTokenAsync(AppUser user, bool rememberMe);
}
