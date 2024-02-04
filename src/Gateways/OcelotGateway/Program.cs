using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;
using SharedApplication.CORS;


var builder = WebApplication.CreateBuilder(args);

var cors = "default";
builder.Services.AddDomainCors(cors);

// Add services to the container.
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration)
        .AddCacheManager(x =>
        {
            x.WithDictionaryHandle();
        });
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
;

var app = builder.Build();





app.UseCors(cors);
app.Use(async (context, next) =>
{
    Console.WriteLine("Hello there!");
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

app.UseOcelot().Wait();


app.Run();
