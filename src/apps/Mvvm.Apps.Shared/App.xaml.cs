using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvvm.Apps.Views;
#if !HAS_WPF
using Uno.UI;
using Uno.Resizetizer;
#endif

namespace Mvvm.Apps;

public sealed partial class App : Application
{
    #region Properties

    private IHost Host { get; }
    
#if !HAS_WPF
    private Window? MainWindow { get; set; }
#endif
    
    #endregion

    #region Constructors

    public App()
    {
        Host = UnoProgram.CreateUnoApp();
        
#if !HAS_WPF
        InitializeComponent();
#endif
    }

    #endregion

    #region Event Handlers

#if HAS_WPF

    private void OnStartup(object sender, StartupEventArgs e)
    {
        new MainView
        {
            DataContext = Host.Services.GetRequiredService<MainViewModel>(),
        }.Show();
    }

#else

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
#if HAS_WINUI && !HAS_UNO
        MainWindow = new Window();
#else
        MainWindow = Microsoft.UI.Xaml.Window.Current;
#endif
        
#if DEBUG
        MainWindow.EnableHotReload();
#endif
        
        if (MainWindow.Content is not Frame frame)
        {
            frame = new Frame();

            MainWindow.Content = frame;

            frame.NavigationFailed += OnNavigationFailed;
        }

        frame.Content ??= new MainView
        {
            ViewModel = Host.Services.GetRequiredService<MainViewModel>(),
        };

        MainWindow.Activate();

        MainWindow.SetWindowIcon();
    }
    
    void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new InvalidOperationException($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
    }
    
#endif


    #endregion
}
