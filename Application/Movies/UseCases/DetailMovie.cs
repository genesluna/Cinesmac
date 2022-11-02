using Application.Movies.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies.UseCases;

public class DetailMovie
{
  public class Query : IRequest<MovieDto>
  {
    public Guid Id { get; set; }
  }

  public class Handler : IRequestHandler<Query, MovieDto>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<MovieDto> Handle(Query request, CancellationToken cancellationToken)
    {
      var movie = await _context.Movies
            .Include(m => m.Sessions)
            .ThenInclude(s => s.ScreeningRoom)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

      var movieDto = _mapper.Map<MovieDto>(movie);

      return movieDto;
    }
  }
}
