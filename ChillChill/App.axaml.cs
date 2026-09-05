using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ChillChill.Services;
using ChillChill.Services.Auth;
using ChillChill.ViewModels;
using ChillChill.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;

namespace ChillChill
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                services.AddSingleton<IApiClient, ApiClient>();
                services.AddSingleton<IAuthSession, AuthSession>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddTransient<AuthHandler>();
                services.AddHttpClient<IApiClient, ApiClient>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7155/");
                }).AddHttpMessageHandler<AuthHandler>();
                
                var scoped = services.BuildServiceProvider().CreateScope();
                MainWindowViewModel vm = scoped.ServiceProvider.GetRequiredService<MainWindowViewModel>();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm,
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}