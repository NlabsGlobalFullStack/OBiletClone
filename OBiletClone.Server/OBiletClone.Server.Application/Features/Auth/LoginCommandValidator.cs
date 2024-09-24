using FluentValidation;

namespace OBiletClone.Server.Application.Features.Auth;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(p => p.UserName)
            .MinimumLength(3)
            .WithMessage("Username or email address must be at least 3 characters.");
        RuleFor(p => p.Password)
            .MinimumLength(1)
            .WithMessage("Password must be at least 1 character");
    }
}