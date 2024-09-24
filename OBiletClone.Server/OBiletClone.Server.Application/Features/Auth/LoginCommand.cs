using MediatR;
using Nlabs.Result;
using OBiletClone.Server.Domain.DTOs.Auth;

namespace OBiletClone.Server.Application.Features.Auth;
public sealed record LoginCommand(
    string UserName,
    string Password,
    bool RememberMe) : IRequest<Result<LoginResponseDto>>;
