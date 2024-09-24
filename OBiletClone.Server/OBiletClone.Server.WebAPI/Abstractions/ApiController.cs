using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OBiletClone.Server.WebAPI.Abstractions;
[Route("[controller]/[action]")]
[ApiController]

[Authorize(AuthenticationSchemes = "Bearer")]
public abstract class ApiController : ControllerBase
{
    public readonly IMediator _mediator;

    protected ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
