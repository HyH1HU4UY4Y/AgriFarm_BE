using SharedApplication;
using SharedApplication.Middleware;
using SharedApplication.Persistence;
using SharedApplication.CORS;
using SharedApplication.Authorize;
using SharedApplication.Serializer;
using SharedApplication.Versioning;
using EventBus;

using Infrastructure.Training;
using Infrastructure.Training.Contexts;
using Service.Supply.Consumers.Replication;
using EventBus.Defaults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cors = "default";
builder.Services.AddDomainCors(cors);

builder.Services.AddInfras(builder.Configuration);
builder.Services.AddSharedApplication<Program>();
builder.Services.AddJWTAuthorization();
builder.Services.AddDefaultVersioning();
builder.Services.AddGlobalErrorMiddleware();


builder.Services.AddControllers()
                .AddDefaultJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultEventBusExtension<Program>(
    builder.Configuration,
    (config, context) =>
    {
        config.AddReceiveEndpoint<TrainingDetailReplicatedConsumer>(EventQueue.TrainingDetailReplicationQueue, context);

    });

var app = builder.Build();

app.EnsureDataInit<TrainingContext>().Wait();
app.UseGlobalErrorMiddleware();
app.SeedData();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
