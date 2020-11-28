﻿using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScheduleMaster.Services;
using ScheduleMaster.ViewModels;

namespace ScheduleMaster
{
    public partial class App : Application
    {
        private static IHost _Host;

        public static IHost Host => _Host 
            ??= Programm.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddViewModel()
            .AddServices()
        ;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            base.OnExit(e);
            await host.StopAsync();
        }
    }
}
