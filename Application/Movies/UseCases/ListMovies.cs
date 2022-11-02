using Application.Movies.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies.UseCases;

public class ListMovies
{
  public class Query : IRequest<List<MovieDto>> { }

  public class Handler : IRequestHandler<Query, List<MovieDto>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<List<MovieDto>> Handle(Query request, CancellationToken cancellationToken)
    {
      var movies = await _context.Movies
              .Include(m => m.Sessions)
              .ThenInclude(s => s.ScreeningRoom)
              .ToListAsync(cancellationToken);

      var moviesDto = _mapper.Map<List<MovieDto>>(movies);
      return moviesDto;
    }
  }
}
