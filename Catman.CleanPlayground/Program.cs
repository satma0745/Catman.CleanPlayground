namespace Catman.CleanPlayground
{
    using Catman.CleanPlayground.Application.Extensions.DependencyInjection;
    using Catman.CleanPlayground.Extensions.DependencyInjection;
    using Catman.CleanPlayground.Persistence.Extensions.DependencyInjection;
    using Catman.CleanPlayground.Presentation;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Program
    {
        private static void Main()
        {
            var serviceProvider = ConfigureServices().BuildServiceProvider();
            
            var userPresentation = serviceProvider.GetService<UsersPresentation>();
            userPresentation!.RunInteraction();
        }

        private static IServiceCollection ConfigureServices() =>
            new ServiceCollection()
                .AddPersistence()
                .AddApplication()
                .AddConsole();
    }
}
