using Application.Movies.Dtos;
using Application.Movies.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseAPIController
{
  [HttpGet]
  public async Task<ActionResult<List<MovieDto>>> GetMovies(CancellationToken cancellationToken)
  {
    return await Mediator.Send(new ListMovies.Query(), cancellationToken);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<MovieDto>> GetMovie(Guid id, CancellationToken cancellationToken)
  {
    return await Mediator.Send(new DetailMovie.Query { Id = id }, cancellationToken);
  }

  [HttpPost]
  public async Task<IActionResult> CreateMovie(MovieCreateDto movie, CancellationToken cancellationToken)
  {
    return Ok(await Mediator.Send(new CreateMovie.Command { Movie = movie }, cancellationToken));
  }

  [HttpPut]
  public async Task<IActionResult> EditMovie(MovieEditDto movie, CancellationToken cancellationToken)
  {
    return Ok(await Mediator.Send(new EditMovie.Command { Movie = movie }, cancellationToken));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteMovie(Guid id, CancellationToken cancellationToken)
  {
    return Ok(await Mediator.Send(new DeleteMovie.Command { Id = id }, cancellationToken));
  }
}
