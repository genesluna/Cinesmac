using Application.Core;
using Application.Movies.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Movies.UseCases;

public class ListMovies
{
  public class Query : IRequest<Result<PagedList<MoviesListDto>>>
  {
    public PagingParameters PagingParams { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<PagedList<MoviesListDto>>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<PagedList<MoviesListDto>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var query = _context.Movies
              .OrderBy(m => m.Title)
              .ProjectTo<MoviesListDto>(_mapper.ConfigurationProvider)
              .AsQueryable();

      var moviesDto = await PagedList<MoviesListDto>.CreateAsync(query, request.PagingParams.Index,
              request.PagingParams.Limit, cancellationToken);

      return Result<PagedList<MoviesListDto>>.Success(moviesDto);
    }
  }
}
