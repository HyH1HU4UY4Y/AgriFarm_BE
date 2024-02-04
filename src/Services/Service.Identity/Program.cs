using SharedApplication;
using SharedApplication.Middleware;
using SharedApplication.CORS;

using EventBus;
using SharedApplication.Authorize;
using Infrastructure.Identity;
using SharedApplication.Persistence;

using Infrastructure.Identity.Contexts;
using Service.Identity.Consumers;
using EventBus.Defaults;
using SharedApplication.Serializer;
using SharedApplication.Versioning;
using MediatR;
using Service.Identity.DTOs;
using Service.Identity.Commands.Users;
using SharedDomain.Defaults;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var cors = "default";
builder.Services.AddDomainCors(cors);

builder.Services.AddInfras(builder.Configuration);
builder.Services.AddSharedApplication<Program>();
//builder.Services.AddAuthModule(builder.Configuration);
builder.Services.AddGlobalErrorMiddleware();
builder.Services.AddDefaultVersioning();
builder.Services.AddAuthModule(builder.Configuration);
builder.Services.AddDefaultEventBusExtension<Program>(
    builder.Configuration,
    (config, context) =>
    {
        config.AddReceiveEndpoint<InitFarmOwnerConsumer>(EventQueue.InitFarmOwnerQueue, context);
    });


builder.Services.AddControllers()
    .AddDefaultJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.EnsureDataInit<IdentityContext>().Wait();

using (var scope = app.Services.CreateScope())
{
    var _m = scope.ServiceProvider.GetRequiredService<IMediator>();
    var _db = scope.ServiceProvider.GetRequiredService<IdentityContext>();

    if (!_db.SiteDetails.Any())
    {

        _db.SiteDetails.Add(new()
        {
            Id = new Guid(TempData.FarmId),
            Name = "site01",
            IsActive = true,
            SiteCode = "site021.abc",

        });
        _db.SaveChanges();
    }
    if (_db.Users.Count() < 2)
    {
        await _m.Send(new CreateMemberCommand
        {
            AccountType = AccountType.Admin,
            FirstName = $"Admin",
            LastName = $"Owner",
            SiteId = new Guid(TempData.FarmId),
            Password = "@123456",
            UserName = $"owner01@farmer"
        });

        for (var i = 1; i <= 5; i++)
        {
            await _m.Send(new CreateMemberCommand
            {
                AccountType = AccountType.Member,
                FirstName = $"User",
                LastName = $"User{i}",
                Password = "@123456",
                SiteId = new Guid(TempData.FarmId),
                UserName = $"Userx0{i}@test.vn"
            });
        }
    }

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseGlobalErrorMiddleware();

//app.UseHttpsRedirection();

app.UseCors(cors);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
