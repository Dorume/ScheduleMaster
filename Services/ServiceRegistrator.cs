using Microsoft.Extensions.DependencyInjection;
using ScheduleMaster.ViewModels;

namespace ScheduleMaster.Services
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddSingleton<MainViewModel>()
        ;
    }
}