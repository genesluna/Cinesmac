using Application.Core;
using Application.Movies.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies.UseCases;

public class DetailMovie
{
  public class Query : IRequest<Result<MovieDto>>
  {
    public Guid Id { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<MovieDto>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<MovieDto>> Handle(Query request, CancellationToken cancellationToken)
    {
      var movie = await _context.Movies
            .Include(m => m.Sessions)
            .ThenInclude(s => s.ScreeningRoom)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

      if (movie == null)
        return Result<MovieDto>.Failure(ErrorType.NotFound, "Movie not found");

      return Result<MovieDto>.Success(_mapper.Map<MovieDto>(movie));
    }
  }
}
