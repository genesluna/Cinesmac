using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Movies.UseCases;

public class DeleteMovie
{
  public class Command : IRequest<Result<Unit>>
  {
    public Guid Id { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
    private readonly DataContext _context;
    public Handler(DataContext context)
    {
      _context = context;
    }

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      var movie = await _context.Movies.FindAsync(request.Id);

      if (movie == null)
        return Result<Unit>.Failure(ErrorType.NotFound, "Movie not found");

      _context.Remove(movie);

      var result = await _context.SaveChangesAsync() > 0;

      if (!result)
        return Result<Unit>.Failure(ErrorType.SaveChangesError, "Failed to delete movie");

      return Result<Unit>.Success(Unit.Value);

    }
  }
}
