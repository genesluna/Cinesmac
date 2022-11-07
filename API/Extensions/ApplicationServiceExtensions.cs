using API.Errors;
using Application.Core;
using Application.Movies.UseCases;
using Application.Movies.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using StackExchange.Redis;

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

    services.Configure<ApiBehaviorOptions>(opt =>
    {
      opt.InvalidModelStateResponseFactory = actionContext =>
      {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
          Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
      };
    });

    services.AddCors(opt =>
    {
      opt.AddPolicy("CorsPolicy", policy =>
      {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("https://localhost:4200");
      });
    });

    services.AddDbContext<DataContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));

    services.AddSingleton<IConnectionMultiplexer>(cfg =>
    {
      var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);

      return ConnectionMultiplexer.Connect(configuration);
    });

    services.AddMediatR(typeof(ListMovies.Handler).Assembly);
    services.AddAutoMapper(typeof(MappingProfiles).Assembly);

    return services;
  }
}
