using Microsoft.AspNetCore.Identity;

namespace OBiletClone.Server.Domain.Entities;
public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
}
