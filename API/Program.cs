using API.Middleware;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddCors();

// Redis configuration with ConnectionMultiplexer by inject ConnectionMultiplexer to DI Container: https://redis.io/learn/develop/dotnet 
builder.Services.AddSingleton<IConnectionMultiplexer>(config => 
{
    var connString = builder.Configuration.GetConnectionString("Redis") 
        ?? throw new Exception("Cannot get redis connection string");
    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddAuthorization();

/* Log registered services from specific extension method */

// Capture initial state of services
var initialServices = builder.Services.ToList();

// Call AddIdentityApiEndpoints
builder.Services.AddIdentityApiEndpoints<AppUser>();

// Capture final state of services
var finalServices = builder.Services.ToList();

// Determine added services
var addedServices = finalServices.Except(initialServices, new ServiceDescriptorComparer());

foreach (var service in addedServices)
{
    Console.WriteLine($"Service: {service.ServiceType.FullName}, Lifetime: {service.Lifetime}, Implementation: {service.ImplementationType?.FullName}");
}



/* Log all registered services in DI Container*/
foreach (var service in builder.Services)
{
    Console.WriteLine($"Service: {service.ServiceType.FullName}, Lifetime: {service.Lifetime}, Implementation: {service.ImplementationType?.FullName} \n");
}

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));



app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
try
{  
    // crerate a scope to get the service provider and get the StoreContext service from it
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();

        // apply the async migrations and seed the database
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context);
    }
}
catch (Exception ex)
{
     Console.WriteLine(ex.StackTrace);
    throw  new Exception(ex.Message);
}

app.Run();


// Custom comparer for ServiceDescriptor
public class ServiceDescriptorComparer : IEqualityComparer<ServiceDescriptor>
{
    public bool Equals(ServiceDescriptor x, ServiceDescriptor y)
    {
        return x.ServiceType == y.ServiceType &&
               x.Lifetime == y.Lifetime &&
               x.ImplementationType == y.ImplementationType;
    }

    public int GetHashCode(ServiceDescriptor obj)
    {
        return HashCode.Combine(obj.ServiceType, obj.Lifetime, obj.ImplementationType);
    }
}