using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ScheduleMaster.ViewModels
{
    class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Host.Services.GetRequiredService<MainViewModel>();
    }
}
