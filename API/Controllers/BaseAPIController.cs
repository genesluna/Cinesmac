using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BaseAPIController : ControllerBase
{
  private IMediator _mediator;
  protected IMediator Mediator => _mediator ??=
    HttpContext.RequestServices.GetService<IMediator>();
}
