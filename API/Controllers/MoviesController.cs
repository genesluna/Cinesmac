using Application.Core;
using Application.Movies.Dtos;
using Application.Movies.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseAPIController
{
  [HttpGet]
  public async Task<ActionResult<PagedList<MovieDto>>> GetMovies([FromQuery] PagingParameters pagingParams,
    CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new ListMovies.Query { PagingParams = pagingParams },
      cancellationToken));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
  {
    return HandleResult(await Mediator.Send(new DetailMovie.Query { Id = id }));
  }

  [HttpPost]
  public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto movie)
  {
    return HandleResult(await Mediator.Send(new CreateMovie.Command { Movie = movie }));
  }

  [HttpPut]
  public async Task<IActionResult> EditMovie(MovieEditDto movie)
  {
    return HandleResult(await Mediator.Send(new EditMovie.Command { Movie = movie }));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteMovie(Guid id)
  {
    return HandleResult(await Mediator.Send(new DeleteMovie.Command { Id = id }));
  }
}
