using SharedApplication;
using SharedApplication.Middleware;

using SharedApplication.CORS;
using SharedApplication.Authorize;

using Infrastructure.Payment;
using SharedApplication.Persistence;
using Infrastructure.Payment.Context;
using System.Reflection;

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
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Version = "v1",
            Title = "AgriFarm Payment service api",
            Description = "AgriFarm .NET payment api",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Name = "AgriFarm",
                Url = new Uri("https://globalfarm.vercel.app/en/register")
            }
        });
        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var path = Path.Combine(AppContext.BaseDirectory, xmlFileName);
        options.IncludeXmlComments(path);
    });

var app = builder.Build();
app.EnsureDataInit<PaymentContext>().Wait();

app.UseGlobalErrorMiddleware();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
