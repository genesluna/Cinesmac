using Application.Movies.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Movies.UseCases;

public class CreateMovie
{
  public class Command : IRequest
  {
    public MovieCreateDto Movie { get; set; }
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
      var movieToAdd = _mapper.Map<Movie>(request.Movie);
      _context.Movies.Add(movieToAdd);
      await _context.SaveChangesAsync();

      return Unit.Value;
    }
  }
}
