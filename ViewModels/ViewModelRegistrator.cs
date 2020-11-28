using Microsoft.Extensions.DependencyInjection;

namespace ScheduleMaster.ViewModels
{
    public  static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModel(this IServiceCollection services) => services
            .AddSingleton<MainViewModel>()
        ;
    }
}