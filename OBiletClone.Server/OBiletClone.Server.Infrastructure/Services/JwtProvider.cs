using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OBiletClone.Server.Domain.DTOs.Auth;
using OBiletClone.Server.Domain.Entities;
using OBiletClone.Server.Domain.Services;
using OBiletClone.Server.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OBiletClone.Server.Infrastructure.Services;
internal class JwtProvider(UserManager<AppUser> userManager, IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    public async Task<LoginResponseDto> CreateTokenAsync(AppUser user, bool rememberMe)
    {
        var claims = new List<Claim>();
        {
            new Claim("Id", user.Id.ToString());
            new Claim("Name", user.FullName);
            new Claim("Email", user.Email ?? "");
            new Claim("UserName", user.UserName ?? "");
        };


        var expires = rememberMe ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddMonths(1);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            );

        var handler = new JwtSecurityTokenHandler();

        var token = handler.WriteToken(jwtSecurityToken);

        var refreshToken = Guid.NewGuid().ToString();

        var refreshTokenExpires = expires.AddHours(1);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;

        await userManager.UpdateAsync(user);

        return new(token, refreshToken, refreshTokenExpires);
    }
}
