using System.Net;
using API.Errors;
using API.Extensions;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseAPIController : ControllerBase
{
  private IMediator _mediator;
  protected IMediator Mediator => _mediator ??=
    HttpContext.RequestServices.GetService<IMediator>();

  protected ActionResult HandleResult<T>(Result<T> result)
  {
    if (result.IsSuccess)
      return Ok(result.Value);
    else
      return HandleError(result.ErrorType, result.ErrorMessage);
  }

  protected ActionResult HandleResult<T>(Result<PagedList<T>> result)
  {
    if (result.IsSuccess)
    {
      Response.AddPaginationsHeaders(result.Value.CurrentPage, result.Value.PageSize,
          result.Value.TotalPages, result.Value.TotalCount);
      return Ok(result.Value);
    }
    else
      return HandleError(result.ErrorType, result.ErrorMessage);
  }

  private ActionResult HandleError(ErrorType errorType, string errorMessage)
  {
    return errorType switch
    {
      ErrorType.NotFound => NotFound(new ApiResponse((int)HttpStatusCode.NotFound, errorMessage)),
      ErrorType.SaveChangesError => BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, errorMessage)),
      _ => BadRequest()
    };
  }
}
