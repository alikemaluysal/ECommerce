using MediatR;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Security.Extensions;
using System.Security.Authentication;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected IMediator Mediator =>
        _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()
            ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _mediator;

    protected string getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }

    protected Guid getUserIdFromRequest() 
    {
        var claim = HttpContext.User.GetIdClaim();

        if (claim is null)
            throw new AuthenticationException("User ID claim is missing in the token.");

        var userId = Guid.Parse(claim);

        return userId;
    }
}
