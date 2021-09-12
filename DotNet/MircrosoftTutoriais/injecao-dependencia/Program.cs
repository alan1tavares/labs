using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace injecao_dependencia
{
  class Program
  {
    static void Main(string[] args)
    {
      using IHost host = CreateHostBuilder(args).Build();

      ExemplifyScoping(host.Services, "Scope 1");
      ExemplifyScoping(host.Services, "Scope 2");

      host.RunAsync();

    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
      {
        services.AddTransient<ITransientOperation, DefaultOperation>();
        services.AddScoped<IScopedOperation, DefaultOperation>();
        services.AddSingleton<ISingletonOperation, DefaultOperation>();
        services.AddTransient<OperationLogger>();
        services.AddHostedService<ExampleHostedService>();
      });
    }
    private static void ExemplifyScoping(IServiceProvider services, string scope)
    {
      using IServiceScope serviceScope = services.CreateScope();
      IServiceProvider provider = serviceScope.ServiceProvider;

      OperationLogger logger = provider.GetRequiredService<OperationLogger>();
      logger.LogOperations($"{scope}-Call 1 .GetRequiredService<OperationLogger>()");

      Console.WriteLine("...");

      logger = provider.GetRequiredService<OperationLogger>();
      logger.LogOperations($"{scope}-Call 2 .GetRequiredService<OperationLogger>()");

      Console.WriteLine();
    }
  }
}
