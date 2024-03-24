using SharedApplication;
using SharedApplication.Middleware;
using EventBus;

using SharedApplication.CORS;
using SharedApplication.Authorize;
using SharedApplication.Serializer;
using SharedApplication.Versioning;
using Infrastructure.FarmScheduling;
using SharedApplication.Persistence;
using Infrastructure.FarmScheduling.Contexts;
using EventBus.Defaults;
using Service.FarmScheduling.Consumers.Replication;

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
        config.AddReceiveEndpoint<PesticideReplicatedConsumer>(EventQueue.PesticideReplicationQueue, context);
        config.AddReceiveEndpoint<FertilizeReplicatedConsumer>(EventQueue.FertilizeReplicationQueue, context);
        config.AddReceiveEndpoint<SeedReplicatedConsumer>(EventQueue.SeedReplicationQueue, context);
        config.AddReceiveEndpoint<LandReplicatedConsumer>(EventQueue.SoilReplicationQueue, context);
        config.AddReceiveEndpoint<EquipmentReplicatedConsumer>(EventQueue.EquipmentReplicationQueue, context);
        config.AddReceiveEndpoint<WaterReplicatedConsumer>(EventQueue.WaterReplicationQueue, context);
        config.AddReceiveEndpoint<UserReplicatedConsumer>(EventQueue.UserReplicationQueue, context);


    });


builder.Services.AddControllers()
                .AddDefaultJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.EnsureDataInit<ScheduleContext>().Wait();
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
