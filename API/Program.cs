using Persistence;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Middleware;
using Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Migrating and Seeding   
using var scope = app.Services.CreateScope();
try
{
  var context = scope.ServiceProvider.GetRequiredService<DataContext>();
  var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
  var identityContext = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();

  await context.Database.MigrateAsync();
  await identityContext.Database.MigrateAsync();
  await DbSeeder.SeedAsync(context);
  await IdentityDbSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
  var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
  logger.LogError(ex, "Problem migrating and seeding data");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

  app.UseMiddleware<ExceptionMiddleware>();
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
