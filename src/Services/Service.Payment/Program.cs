using SharedApplication;
using SharedApplication.Middleware;

using SharedApplication.CORS;
using SharedApplication.Authorize;

using Infrastructure.Payment;
using SharedApplication.Persistence;
using Infrastructure.Payment.Context;
using Infrastructure.Payment.VnPay.Config;
using Service.Payment.Interface;
using Service.Payment.Services;
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
builder.Services.AddSwaggerGen();


builder.Services.Configure<VnpayConfig>(
    builder.Configuration.GetSection(VnpayConfig.ConfigName));

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
