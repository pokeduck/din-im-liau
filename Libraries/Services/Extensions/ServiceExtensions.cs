
using Microsoft.Extensions.DependencyInjection;

namespace Services.Extensions;

public static class ServiceExtensions
{
  public static void AddServices(this IServiceCollection services)
  {
    Console.WriteLine("do something...");
    Console.WriteLine(services);
  }
}
