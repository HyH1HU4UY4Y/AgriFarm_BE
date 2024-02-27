using SharedApplication;
using SharedApplication.Persistence;
using SharedApplication.Middleware;

using SharedApplication.CORS;
using SharedApplication.Authorize;
using EventBus;

using Infrastructure.Seed;
using Infrastructure.Seed.Contexts;
using SharedApplication.Serializer;
using SharedApplication.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cors = "default";
builder.Services.AddDomainCors(cors);

builder.Services.AddInfras(builder.Configuration);
builder.Services.AddSharedApplication<Program>();
builder.Services.AddJWTAuthorization();
builder.Services.AddDefaultVersioning();
builder.Services.AddGlobalErrorMiddleware();


builder.Services.AddDefaultEventBusExtension<Program>(
    builder.Configuration,
    (config, context) =>
    {

    });

builder.Services.AddControllers()
                .AddDefaultJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.EnsureDataInit<FarmSeedContext>().Wait();
app.UseGlobalErrorMiddleware();
app.SeedData();

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
