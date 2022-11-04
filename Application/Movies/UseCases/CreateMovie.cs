using Application.Core;
using Application.Movies.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Movies.UseCases;

public class CreateMovie
{
  public class Command : IRequest<Result<MovieDto>>
  {
    public MovieCreateDto Movie { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<MovieDto>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<MovieDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      var movieToAdd = _mapper.Map<Movie>(request.Movie);
      _context.Movies.Add(movieToAdd);

      var result = await _context.SaveChangesAsync() > 0;

      if (!result)
        return Result<MovieDto>.Failure(ErrorType.SaveChangesError, "Failed to create movie");

      return Result<MovieDto>.Success(_mapper.Map<MovieDto>(movieToAdd));
    }
  }
}
