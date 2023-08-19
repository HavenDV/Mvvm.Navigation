using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvvm.Apps.ViewModels;
using Mvvm.Apps.Views;
using Mvvm.Navigation;

namespace Mvvm.Apps;

public class App : Application
{
    #region Properties

    private IHost? AppHost { get; set; }

    #endregion

    public override void Initialize()
    {
        AppHost = Host
            .CreateDefaultBuilder()
            .ConfigureServices(static services =>
            {
                services.AddSingleton<IFileInteractions, FileInteractions>();
                services.AddSingleton<IMessageInteractions, MessageInteractions>();
                services.AddSingleton<IWebInteractions, WebInteractions>();
            })
            .Build();

        AvaloniaXamlLoader.Load(this);

        AppHost.Services.GetRequiredService<IMessageInteractions>().CatchUnhandledExceptions(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var view = new MainView
            {
                DataContext = new MainViewModel(AppHost?.Services ?? throw new InvalidOperationException(nameof(AppHost.Services))),
            };
            desktop.MainWindow = view;
            FileInteractions.Window = view;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
