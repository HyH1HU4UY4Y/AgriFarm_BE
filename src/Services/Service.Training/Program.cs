using SharedApplication;
using SharedApplication.Middleware;
using SharedApplication.Persistence;
using SharedApplication.CORS;
using SharedApplication.Authorize;

using Infrastructure.Training;
using Infrastructure.Training.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cors = "default";
builder.Services.AddDomainCors(cors);

builder.Services.AddInfras(builder.Configuration);
builder.Services.AddSharedApplication<Program>();
builder.Services.AddJWTAuthorization();
builder.Services.AddGlobalErrorMiddleware();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.EnsureDataInit<TrainingContext>().Wait();

app.UseSwagger();
app.UseSwaggerUI();

app.UseGlobalErrorMiddleware();

app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
