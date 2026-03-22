using Esperanca.Identity.Infrastructure._Shared;
using Esperanca.Identity.WebApi;
using Esperanca.Identity.WebApi._Shared.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

IdentityWebApiModule.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Esperanca Identity API v1"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

await DatabaseSeed.SeedAdminAsync(app.Services);

app.Run();

public partial class Program;
