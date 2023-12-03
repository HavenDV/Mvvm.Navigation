using Avalonia;
using Avalonia.Headless;
using Avalonia.Themes.Fluent;
using Mvvm.Navigation.IntegrationTests;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace Mvvm.Navigation.IntegrationTests;

public class App : Application
{
    public App()
    {
        Styles.Add(new FluentTheme());
    }
}

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder
            .Configure<App>()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions());
    }
}