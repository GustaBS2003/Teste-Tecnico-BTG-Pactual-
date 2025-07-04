using BTGProject.Core;
using BTGProject.Core.Services;
using BTGProject.Core.Services.Interfaces;
using BTGProject.Core.ViewModels;
using BTGProject.Services;
using BTGProject.Views;
using Microsoft.Extensions.Logging;

namespace BTGProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Register services and settings
            builder.Services.AddSingleton<AppSettings>();
            builder.Services.AddSingleton<IClientService, ClientService>();

            // Serviços de UI (injeção das abstrações)
            builder.Services.AddSingleton<IAlertService, MauiAlertService>();
            builder.Services.AddSingleton<INavigationService, MauiNavigationService>();

            // Register ViewModels
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<ClientEditViewModel>();

            // Register Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ClientEditPage>();

            return builder.Build();
        }
    }
}
