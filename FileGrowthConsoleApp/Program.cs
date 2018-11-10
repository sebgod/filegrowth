using Microsoft.Extensions.DependencyInjection;
using FileGrowthService.App;

namespace FileGrowthConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var application = new Application(serviceCollection);

            application.ProcessFiles();
        }
    }
}
