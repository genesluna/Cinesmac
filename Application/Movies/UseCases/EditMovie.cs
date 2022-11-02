using Application.Movies.Dtos;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Movies.UseCases;

public class EditMovie
{
  public class Command : IRequest
  {
    public MovieEditDto Movie { get; set; }
  }

  public class Handler : IRequestHandler<Command>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
      var movie = await _context.Movies.FindAsync(request.Movie.Id);

      _mapper.Map(request.Movie, movie);

      await _context.SaveChangesAsync();

      return Unit.Value;
    }
  }
}
