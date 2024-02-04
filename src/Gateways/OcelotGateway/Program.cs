using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
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



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
