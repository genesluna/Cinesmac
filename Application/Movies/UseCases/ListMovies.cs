using Application.Core;
using Application.Movies.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies.UseCases;

public class ListMovies
{
  public class Query : IRequest<Result<PagedList<MovieDto>>>
  {
    public PagingParameters PagingParams { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<PagedList<MovieDto>>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<PagedList<MovieDto>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var query = _context.Movies
              .OrderBy(m => m.Title)
              .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
              .AsQueryable();

      var moviesDto = await PagedList<MovieDto>.CreateAsync(query, request.PagingParams.Index,
              request.PagingParams.Limit, cancellationToken);

      return Result<PagedList<MovieDto>>.Success(moviesDto);
    }
  }
}
