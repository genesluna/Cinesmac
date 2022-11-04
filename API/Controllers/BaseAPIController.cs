using System.Net;
using API.Errors;
using Application.Core;
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

  protected ActionResult HandleResult<T>(Result<T> result)
  {
    if (result.IsSuccess)
      return Ok(result.Value);
    else
    {
      return result.ErrorType switch
      {
        ErrorType.NotFound => NotFound(new ApiResponse((int)HttpStatusCode.NotFound, result.ErrorMessage)),
        ErrorType.SaveChangesError => BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.ErrorMessage)),
        _ => BadRequest()
      };
    }
  }

  protected ActionResult HandleResult<T>(Result<PagedList<T>> result)
  {
    return BadRequest();
  }
}
