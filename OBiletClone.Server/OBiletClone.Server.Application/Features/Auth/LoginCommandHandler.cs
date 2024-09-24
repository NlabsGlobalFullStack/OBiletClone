using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nlabs.Result;
using OBiletClone.Server.Domain.DTOs.Auth;
using OBiletClone.Server.Domain.Entities;
using OBiletClone.Server.Domain.Services;

namespace OBiletClone.Server.Application.Features.Auth;

internal sealed class LoginCommandHandler(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
{
    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == request.UserName, cancellationToken);

        if (user is null)
        {
            return (500, "User not found!");
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            var timeSpan = user.LockoutEnd - DateTime.UtcNow;

            if (timeSpan is not null)
                return (500, $"User has been blocked for {Math.Ceiling(timeSpan.Value.TotalMinutes)} minutes because you entered your password incorrectly 3 times.");
            else
                return (500, "Your user has been blocked for 5 minutes because they entered the wrong password 3 times.");
        }

        if (signInResult.IsNotAllowed)
        {
            return (500, "Your email address is not confirmed");
        }

        if (!signInResult.Succeeded)
        {
            return (500, "Your password is incorrect");
        }

        var loginResponse = await jwtProvider.CreateTokenAsync(user, request.RememberMe);

        return loginResponse;
    }
}
