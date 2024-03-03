using SharedApplication;
using SharedApplication.Middleware;

using SharedApplication.CORS;
using Infrastructure.FarmSite;
using SharedApplication.Authorize;
using EventBus;
using Service.FarmSite.Consumers;
using EventBus.Defaults;
using SharedApplication.Persistence;
using Infrastructure.FarmSite.Contexts;
using SharedApplication.Serializer;
using SharedApplication.Versioning;

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
    config.AddReceiveEndpoint<FarmRegistedSuccessConsumer>(EventQueue.RegistFarmQueue, context);
});


builder.Services.AddControllers()
    .AddDefaultJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.EnsureDataInit<SiteContext>().Wait();
app.SeedData();

app.UseSwagger();
app.UseSwaggerUI();


app.UseGlobalErrorMiddleware();

//app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
