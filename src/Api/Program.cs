using Api.Extensions;
using Api.Properties;
using ExceptionCatcherMiddleware.Extensions;
using Infrastructure.Mappers.BasketEntity;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ParametersProvider parameters = new(builder.Configuration);

services.AddRepository(parameters.BasketRepositoryOptions);
services.AddMappers();
services.AddExceptionMappers();
services.AddRedis(parameters.Redis);
services.AddConfiguredSwaggerGen();
builder.AddSerilog(parameters.Seq);

services.AddEndpointsApiExplorer();
services.AddControllers();

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionCatcherMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
