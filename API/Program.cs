using Persistence;
using Microsoft.EntityFrameworkCore;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Migrating and Seeding   
using var scope = app.Services.CreateScope();
try
{
  var context = scope.ServiceProvider.GetRequiredService<DataContext>();
  await context.Database.MigrateAsync();
  await DbSeeder.Seed(context);
}
catch (Exception ex)
{
  var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
  logger.LogError(ex, "Problem migrating and seeding data");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
