using SharedApplication;
using SharedApplication.Persistence;
using SharedApplication.Middleware;

using SharedApplication.CORS;
using SharedApplication.Authorize;
using EventBus;

using Infrastructure.Supply;
using Infrastructure.Supply.Contexts;
using Service.Supply.Consumers;
using EventBus.Defaults;
using Service.Supply.Consumers.Replication;
using SharedApplication.Serializer;
using SharedApplication.Versioning;
using Service.Supply.Consumers.Supplying;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cors = "default";
builder.Services.AddDomainCors(cors);

builder.Services.AddInfras(builder.Configuration);
builder.Services.AddSharedApplication<Program>();
builder.Services.AddJWTAuthorization();
builder.Services.AddGlobalErrorMiddleware();
builder.Services.AddDefaultVersioning();

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


        config.AddReceiveEndpoint<PesticideSupplyingConsumer>(EventQueue.PesticideSupplyingQueue, context);
        config.AddReceiveEndpoint<FertilizeSupplyingConsumer>(EventQueue.FertilizeSupplyingQueue, context);
        config.AddReceiveEndpoint<SeedSupplyingConsumer>(EventQueue.SeedSupplyingQueue, context);
        config.AddReceiveEndpoint<LandSupplyingConsumer>(EventQueue.LandSupplyingQueue, context);
        config.AddReceiveEndpoint<EquipmentSupplyingConsumer>(EventQueue.EquipmentSupplyingQueue, context);
    });

builder.Services.AddControllers()
                .AddDefaultJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.EnsureDataInit<SupplyContext>().Wait();

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
