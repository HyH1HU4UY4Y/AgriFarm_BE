using SharedApplication;
using SharedApplication.Middleware;
using SharedApplication.CORS;

using EventBus;
using SharedApplication.Authorize;
using Infrastructure.Identity;
using SharedApplication.Persistence;

using Infrastructure.Identity.Contexts;
using Service.Identity.Consumers;
using EventBus.Defaults;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var cors = "default";
builder.Services.AddDomainCors(cors);

builder.Services.AddInfras(builder.Configuration);
builder.Services.AddSharedApplication<Program>();
//builder.Services.AddAuthModule(builder.Configuration);
builder.Services.AddGlobalErrorMiddleware();

builder.Services.AddDefaultEventBusExtension<Program>(
    builder.Configuration,
    (config, context) =>
    {
        config.AddReceiveEndpoint<InitFarmOwnerConsumer>(EventQueue.InitFarmOwner, context);
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.EnsureDataInit<IdentityContext>().Wait();

app.UseSwagger();
app.UseSwaggerUI();

app.UseGlobalErrorMiddleware();

app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
