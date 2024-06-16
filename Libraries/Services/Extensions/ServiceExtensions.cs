
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Services.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        Console.WriteLine("do something...");
        Console.WriteLine(services);
    }

    public static void RegisterBaseServices(this IServiceCollection services)
    {
        var baseServiceType = typeof(BaseService<>);
        services.Scan(scan => scan.FromAssembliesOf(baseServiceType)
        .AddClasses(classes => classes.AssignableTo(baseServiceType))
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsSelf()
        .WithScopedLifetime());

    }
}
