using Application.Core;
using Application.Movies.UseCases;
using Application.Movies.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddFluentValidationAutoValidation();
    services.AddValidatorsFromAssemblyContaining<MovieCreateValidator>();
    services.AddSwaggerGen();
    services.AddCors(opt =>
    {
      opt.AddPolicy("CorsPolicy", policy =>
      {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
      });
    });

    services.AddDbContext<DataContext>(opt =>
    {
      opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });

    services.AddMediatR(typeof(ListMovies.Handler).Assembly);
    services.AddAutoMapper(typeof(MappingProfiles).Assembly);

    return services;
  }
}
