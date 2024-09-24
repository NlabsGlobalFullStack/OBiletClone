using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OBiletClone.Server.Application.Features.Auth;
using OBiletClone.Server.WebAPI.Abstractions;

namespace OBiletClone.Server.WebAPI.Controllers;

[AllowAnonymous]
public sealed class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
