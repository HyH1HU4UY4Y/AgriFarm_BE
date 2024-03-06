using SharedApplication;
using SharedApplication.Middleware;

using SharedApplication.CORS;
using SharedApplication.Authorize;

using Infrastructure.Payment;
using SharedApplication.Persistence;
using Infrastructure.Payment.Context;
using System.Reflection;
using SharedDomain.Repositories.Base;
using SharedApplication.Persistence.Repositories;
using Infrastructure.Payment.VnPay.Config;
using MediatR;
using Service.Payment.Commands.MerchantCommands;
using Service.Payment.Interface;
using Service.Payment.Services;
using Hangfire;
using SharedApplication.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cors = "default";
builder.Services.AddDomainCors(cors);

builder.Services.AddInfras(builder.Configuration);
builder.Services.AddSharedApplication<Program>();
builder.Services.AddJWTAuthorization();
builder.Services.AddGlobalErrorMiddleware();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddDefaultVersioning();


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

/*builder.Services.AddMediatR(r =>
{
    r.RegisterServicesFromAssembly(typeof(CreateMerchantCommand).Assembly);
});*/

builder.Services.Configure<VnpayConfig>(
    builder.Configuration.GetSection(VnpayConfig.ConfigName));

/*builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("NSql"),
                new Hangfire.SqlServer.SqlServerStorageOptions()
                {
                    //TODO: Change hangfire sql server option
                }));
builder.Services.AddHangfireServer();*/

var app = builder.Build();
app.EnsureDataInit<PaymentContext>().Wait();

app.UseGlobalErrorMiddleware();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
//app.UseHangfireDashboard();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
