using Application.Core;
using Application.Movies.Dtos;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Movies.UseCases;

public class EditMovie
{
  public class Command : IRequest<Result<Unit>>
  {
    public MovieEditDto Movie { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      var movie = await _context.Movies.FindAsync(request.Movie.Id);

      if (movie == null)
        return Result<Unit>.Failure(ErrorType.NotFound, "Movie not found");

      _mapper.Map(request.Movie, movie);

      var result = await _context.SaveChangesAsync() > 0;

      if (!result)
        return Result<Unit>.Failure(ErrorType.SaveChangesError, "Failed to edit movie");

      return Result<Unit>.Success(Unit.Value);
    }
  }
}
