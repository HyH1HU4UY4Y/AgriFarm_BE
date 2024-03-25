using SharedApplication;
using SharedApplication.Middleware;
using EventBus;

using SharedApplication.CORS;
using SharedApplication.Authorize;

using Infrastructure.Disease;
using SharedApplication.Persistence;
using Infrastructure.RiskAssessment.Context;
using System.Text.Json.Serialization;
using SharedApplication.Versioning;
using Service.RiskAssessment.Consumers;
using EventBus.Defaults;

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
        config.AddReceiveEndpoint<RiskMappingConsumer>(EventQueue.RiskMappingTrackingQueue, context);

    });


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.EnsureDataInit<RiskAssessmentContext>().Wait();

app.UseGlobalErrorMiddleware();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
